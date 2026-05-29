using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using GBP.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBP.Security.Middlewares
{
    public class SecurityMiddleware(RequestDelegate next)
    {
        /// <summary>
        /// Gère les requêtes entrantes pour vérifier si 
        /// l'adresse IP du client est bannie avant de permettre 
        /// l'accès aux ressources protégées.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Réponse HTTP</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // Récupérer l'adresse IP du client
            var ip = GetClientIp(context);

            // Récupérer le référentiel de logs de sécurité
            // Note: On utilise GetService ici pour éviter
            // une dépendance directe à l'interface dans le constructeur du middleware
            // Les middlewares sont Singletons, donc on ne peut pas injecter des services
            // à durée de vie plus courte (comme les Scoped) directement dans le constructeur
            var repository = context
                .RequestServices
                .GetService<ISecurityLogRepository>();

            // Si le service n'est pas disponible, on ne peut pas faire de vérification, donc on continue
            if (repository == null) return;

            // Vérifier si l'IP est bannie
            if (await repository.IsIpBannedAsync(ip))
            {
                // Si l'IP est bannie, retourner une réponse 403 Forbidden
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                // Retourner un message d'erreur en JSON
                var json = JsonSerializer.Serialize(new
                {
                    error = "Accès refusé",
                    message = "Votre adresse IP est bannie due à un excès de connexion infructueuse."
                });

                // Écrire la réponse JSON
                await context.Response.WriteAsync(json);

                return; // Ne pas appeler le middleware suivant
            }

            // Ajouter les headers de sécurité pour renforcer
            // la protection contre les attaques courantes
            AddSecurityHeaders(context);

            // Appel du middleware suivant dans la pipeline
            await next(context);

            // Si la réponse est un code d'erreur (4xx ou 5xx),
            // on peut envisager de logger cette tentative
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized
                && IsSensitiveEndPoint(context.Request.Path))
            {
                await repository.LogAttemptAsync(
                    ip, 
                    context.Request.Path.Value ?? string.Empty,
                    context.Request.Headers["User-Agent"].ToString());
            }
        }

        /// <summary>
        /// Retourne true si le point d'accès est considéré comme sensible 
        /// (ex: endpoints d'authentification)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool IsSensitiveEndPoint(PathString path)
        {
            return path.StartsWithSegments("/api/Auth");
        }

        /// <summary>
        /// Ajoute des en-têtes de sécurité HTTP pour renforcer la protection contre les attaques courantes
        /// </summary>
        /// <param name="context"></param>
        private static void AddSecurityHeaders(HttpContext context)
        {
            var h = context.Request.Headers;
            // Empêche le clickjacking
            h["X-Frame-Options"] = "DENY";
            // Empêche le sniffing de type de contenu
            h["X-Content-Type-Options"] = "nosniff";
            // Active la protection contre les XSS dans les navigateurs
            h["X-XSS-Protection"] = "1; mode=block";
            // Force l'utilisation de HTTPS
            h["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains";
            // Ne pas envoyer le référent 
            h["Referrer-Policy"] = "no-referrer";
        }

        /// <summary>
        /// Récupère l'adresse IP du client en tenant compte des proxys 
        /// (via l'en-tête X-Forwarded-For) et de la connexion directe.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Adresse ip</returns>
        private static string GetClientIp(HttpContext context)
        {
            // Récupérer l'adresse IP du client
            var forwardedIp = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            // Si l'en-tête X-Forwarded-For est présent,
            // prendre la première adresse IP (en cas de plusieurs proxies)
            return forwardedIp?.Split(',').FirstOrDefault()?.Trim() 
                ?? context.Connection.RemoteIpAddress?.ToString() 
                ?? "Unknown";
        }
    }
}