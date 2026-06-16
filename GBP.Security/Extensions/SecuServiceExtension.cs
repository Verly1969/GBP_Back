using GBP.Core.Interfaces.Repositories;
using GBP.Core.Interfaces.Services.Auth;
using GBP.Core.Interfaces.Services.Data;
using GBP.Core.Interfaces.Services.Tools;
using GBP.Infra.Database.Context;
using GBP.Infra.Repositories;
using GBP.Security.Middlewares;
using GBP.Security.Services.Auth;
using GBP.Security.Services.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Extensions
{
    public static class SecuServiceExtension
    {
        /// <summary>
        /// Ajoute les services de sécurité à l'injection de dépendances.
        /// </summary>
        /// <param name="services">La collection de services.</param>
        /// <param name="configuration">La configuration de l'application.</param>
        public static void AddSecuService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("GbpDb");

            services.AddDbContext<GbpDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
            services.AddScoped<ICreditTypeRepository, CreditTypeRepository>();
            services.AddScoped<ICreditRepository, CreditRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountTypeService, AccountTypeService>();
            services.AddScoped<ICreditTypeService, CreditTypeService>();
            services.AddScoped<ICreditService, CreditService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ITotpService, TotpService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ISecurityLogRepository, SecurityLogRepository>();
            services.AddMemoryCache();

            // Configuration des options de JWT à partir du fichier de configuration
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // Valide l'émetteur du token
                        ValidateAudience = true, // Valide le destinataire du token
                        ValidateLifetime = true, // Valide la durée de vie du token
                        ValidateIssuerSigningKey = true, // Valide la clé de signature de l'émetteur
                        ValidIssuer = configuration["Jwt:Issuer"], // L'émetteur du token ("GBP")
                        ValidAudience = configuration["Jwt:Audience"], // Le destinataire du token ("GBP")
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)) // La clé de signature du token (ex: "MyAppSecret")
                    };
                });

            services.AddAuthorization(); // Ajoute les services d'autorisation pour permettre l'utilisation de [Authorize] dans les contrôleurs
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
