using GBP.Core.Interfaces.Repositories;
using GBP.Core.Interfaces.Services.Auth;
using GBP.Infra.Repositories;
using GBP.Security.Middlewares;
using GBP.Security.Services.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Extensions
{
    public static class SecuServiceExtension
    {
        public static void AddSecuService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISecurityLogRepository, SecurityLogRepository>();
            services.AddMemoryCache();
        }

        // Activer le middleware de sécurité dans le pipeline de requêtes
        public static void UseSecuMiddleware(this IApplicationBuilder application)
        {
            // Note: Le middleware lui-même n'est pas enregistré en tant que service
            // car il est ajouté directement dans le pipeline de requêtes via
            // app.UseMiddleware<SecurityMiddleware>()
            // Cependant, les services dont le middleware dépend (comme ISecurityLogRepository)
            // doivent être enregistrés
            application.UseMiddleware<SecurityMiddleware>();
        }
    }
}
