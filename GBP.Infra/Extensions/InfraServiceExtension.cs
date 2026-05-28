using GBP.Core.Interfaces.Repositories;
using GBP.Infra.Database.Context;
using GBP.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Extensions
{
    public static class InfraServiceExtension
    {
        public static void AddInfraServices(this IServiceCollection services , IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("GbpDb");

            services.AddDbContext<GbpDbContext>(options =>
                options.UseSqlServer(connectionString)
            );
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
