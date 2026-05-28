using GBP.Core.Interfaces.Services;
using GBP.Security.Services.Auth;
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
        }
    }
}
