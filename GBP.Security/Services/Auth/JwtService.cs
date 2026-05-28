using GBP.Core.Interfaces.Services.Tools;
using GBP.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GBP.Security.Services.Auth
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        private readonly string _jwtSecret = configuration["Jwt:Secret"] 
            ?? throw new InvalidOperationException("JWT secret is not configured.");
        private readonly string _jwtIssuer = configuration["Jwt:Issuer"] ?? "GBP";
        private readonly string _jwtAudience = configuration["Jwt:Audience"] ?? "GBP";
        private readonly int _jwtExpirationMinutes = int.Parse(
            configuration["Jwt:ExpirationMinutes"] ?? "60");

        public string GenerateToken(User user)
        {
            // 1. Les claims - les informations que nous voulons inclure dans le token
            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Subject - l'identifiant de l'utilisateur
                new(JwtRegisteredClaimNames.Email, user.Email), // Email de l'utilisateur
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID - un identifiant unique pour ce token
                new(ClaimTypes.Role, user.Role.ToString()) // Rôle de l'utilisateur (Admin, User, etc.)
            };

            // 2. La clé de signature - pour signer le token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. La création du token
            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtExpirationMinutes),
                signingCredentials: creds
            );

            // 4. La génération du token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
