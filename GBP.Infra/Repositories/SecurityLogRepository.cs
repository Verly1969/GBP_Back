using GBP.Core.Interfaces.Repositories;
using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Repositories
{
    public class SecurityLogRepository(
        GbpDbContext context,
        IMemoryCache cache) : ISecurityLogRepository
    {
        private const string CachePrefix = "ipban:";
        private const int CacheDuration = 1; // Durée du cache en minutes
        private const int BanThreshold = 10; // Nombre de tentatives avant bannissement
        private const int BanDuration = 30; // Durée du bannissement en minutes
        private const int WindowMinutes = 15; // Fenêtre de temps pour compter les tentatives

        /// <summary>
        /// Bannissement manuel d'une adresse IP.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="raison"></param>
        /// <param name="bannedBy"></param>
        /// <param name="durationMinutes"></param>
        /// <returns></returns>
        public async Task BanIpAsync(
            string ipAddress, string raison, string bannedBy, int? durationMinutes = null)
        {
            // Ne pas bannir une IP déjà bannie
            var existingBan = await context.SecurityLogs
                .FirstOrDefaultAsync(
                s => s.IpAddress == ipAddress && s.IsBanned && (s.EndBan == null || s.EndBan > DateTime.UtcNow));
            if (existingBan != null)
            {
                return;
            }

            context.SecurityLogs.Add(new SecurityLog
            {
                IpAddress = ipAddress,
                EndPoint = "Ban",
                DateAttempt = DateTime.UtcNow,
                IsBanned = true,
                StartBan = DateTime.UtcNow,
                EndBan = durationMinutes.HasValue ? DateTime.UtcNow.AddMinutes(durationMinutes.Value) : null, // Si null, le bannissement est permanent
                BanRaison = raison,
                CreatedBy = bannedBy
            });

            await context.SaveChangesAsync();

            // Invalider le cache pour cette IP
            var expiry = durationMinutes.HasValue 
                ? TimeSpan.FromMinutes(durationMinutes.Value) 
                : TimeSpan.FromDays(365); // Si bannissement permanent, garder en cache longtemps

            cache.Set($"{CachePrefix}{ipAddress}", true, expiry);
        
        }

        /// <summary>
        /// Vérifie si une adresse IP est actuellement bannie.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<bool> IsIpBannedAsync(string ipAddress)
        {
            // Vérifier d'abord le cache
            var cacheKey = $"{CachePrefix}{ipAddress}";
            if (cache.TryGetValue(cacheKey, out bool cached))
            {
                return cached;
            }

            // Vérifier la base de données
            var isBanned = await context.SecurityLogs
                .AnyAsync(s => s.IpAddress == ipAddress && s.IsBanned && (s.EndBan == null || s.EndBan > DateTime.UtcNow));

            // Mettre à jour le cache
            // permet de réduire les appels à la base de données pour les IPs fréquemment vérifiées
            cache.Set(cacheKey, isBanned, TimeSpan.FromMinutes(CacheDuration));
            return isBanned;
        }

        /// <summary>
        /// Traque les tentatives d'accès à l'API. 
        /// Enregistre chaque tentative dans la base de données avec l'adresse IP, le point d'accès, 
        /// l'agent utilisateur et la date de la tentative.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="endPoint"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public async Task LogAttemptAsync(string ipAddress, string endPoint, string? userAgent)
        {
            // Enregistrer la tentative dans la base de données
            context.SecurityLogs.Add(new SecurityLog
            {
                IpAddress = ipAddress,
                EndPoint = endPoint,
                UserAgent = userAgent,
                DateAttempt = DateTime.UtcNow,
                IsBanned = false
            });

            await context.SaveChangesAsync();

            // Vérifier le nombre de tentatives récentes pour cette IP
            var window = DateTime.UtcNow.AddMinutes(-WindowMinutes);
            var nbAttempts = await context.SecurityLogs
                .CountAsync(s => s.IpAddress == ipAddress && s.DateAttempt >= window && !s.IsBanned);

            // Si le nombre de tentatives dépasse le seuil, bannir l'IP
            if (nbAttempts >= BanThreshold)
            {
                await BanIpAsync(ipAddress, "Trop de tentatives échouées", "System", BanDuration);
            }
        }

        /// <summary>
        /// Débannissement manuel d'une adresse IP. 
        /// Met à jour les entrées de la base de données pour marquer l'IP comme débannie et invalide le cache associé.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="unbannedBy"></param>
        /// <returns></returns>
        public async Task UnbanIpAsync(string ipAddress, string unbannedBy)
        { 
            var bans = await context.SecurityLogs
                .Where(s => s.IpAddress == ipAddress && s.IsBanned)
                .ToListAsync();

            foreach (var ban in bans)
            {
                ban.IsBanned = false;
                ban.EndBan = DateTime.UtcNow;
                ban.BanRaison += $" | Unbanned by {unbannedBy} at {DateTime.UtcNow}";
            }

            await context.SaveChangesAsync();

            // Invalider le cache pour cette IP
            cache.Remove($"{CachePrefix}{ipAddress}");
        }
    }
}
