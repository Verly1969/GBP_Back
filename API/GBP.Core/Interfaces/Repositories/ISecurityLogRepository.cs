using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Repositories
{
    public interface ISecurityLogRepository
    {
        // Vérification si une adresse IP est bannie
        Task<bool> IsIpBannedAsync(string ipAddress);

        // Enregistrement d'une tentative de connexion échouée
        Task LogAttemptAsync(string ipAddress, string endPoint, string? userAgent);

        // Bannissement manuelle d'une adresse IP
        Task BanIpAsync(string ipAddress, string raison, string bannedBy, int? durationMinutes = null);

        // Débannissement manuel d'une adresse IP
        Task UnbanIpAsync(string ipAddress, string unbannedBy);
    }
}
