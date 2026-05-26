using GBP.Core.Interfaces.Repositories;
using GBP.Core.Interfaces.Services;
using GBP.Infra.Database.Context;
using GBP.Infra.Repositories;
using GBP.Security.Services.Auth;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<GbpDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GbpDb")
        )
    );

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Gestion Budget Personnel API";
        options.ForceDarkMode();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
