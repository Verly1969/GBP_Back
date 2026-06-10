using GBP.Core.DTOs.Response;
using GBP.Core.Interfaces.Repositories;
using GBP.Core.Interfaces.Services.Auth;
using GBP.Core.Interfaces.Services.Tools;
using GBP.Domain.Entities;
using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Services.Auth
{
    public class AuthService(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IJwtService jwtService,
        ITotpService totpService) : IAuthService
    {
        /// <summary>
        /// Cette méthode gère la vérification du code 2FA pour un utilisateur donné.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns>Le DTO de réponse pour la connexion</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<LoginResponseDto> VerifyTwofactorAsync(string email, string code)
        {
            // 1. Vérifier que l'utilisateur existe
            var user = await userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // 2. Vérifier qu'il a une clé secrète configurée
            if (string.IsNullOrEmpty(user.SecretKeyHash))
            {
                throw new UnauthorizedAccessException("Two-factor authentication is not configured for this user");
            }

            // 3. Vérifier le code 2FA
            if (!totpService.ValidateCode(user.SecretKeyHash, code))
            {
                throw new UnauthorizedAccessException("Invalid two-factor authentication code");
            }

            // 4. Mettre à jour la date de dernière connexion
            user.LastConnected = DateTime.UtcNow;
            await userRepository.UpdateAsync(user);

            // 5. Générer le token JWT
            var token = jwtService.GenerateToken(user);
            var tokenExpiration = DateTime.UtcNow.AddMinutes(60);

            // 6. Retourner le DTO de réponse pour la connexion
            return new LoginResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role.ToString(),
                AccessToken = token,
                AccessTokenExpiration = tokenExpiration
            };
        }

        /// <summary>
        /// Cette méthode gère la logique de connexion d'un utilisateur, 
        /// y compris la vérification de l'existence de l'utilisateur, de son statut, 
        /// de son mot de passe et de la génération du token JWT. 
        /// Si c'est la première connexion, elle génère également une clé secrète pour 2FA 
        /// et retourne le QR code correspondant. 
        /// Pour les connexions suivantes, elle vérifie le code 2FA avant de générer le token JWT.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Le DTO de réponse pour la connexion</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<LoginResponseDto> LoginAsync(string email, string password)
        {
            Console.WriteLine($"Email reçu: {email}");
            Console.WriteLine($"Password reçu: {password}");

            // 1. Vérifier que l'utilisateur existe
            var user = await userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            Console.WriteLine($"User trouvé: {user != null}");

            // 2. Vérifier que l'utilisateur est actif
            if (user.Status != Status.Active)
            {
                throw new UnauthorizedAccessException("User account is not active");
            }

            Console.WriteLine($"Status: {user.Status}");
            Console.WriteLine($"PasswordHash en base: {user.PasswordHash}");


            // 3. Vérifier le mot de passe
            if (!passwordService.VerifyPassword(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var isValid = passwordService.VerifyPassword(password, user.PasswordHash);
            Console.WriteLine($"Password valide : {isValid}");

            if (!isValid)
                throw new UnauthorizedAccessException("Invalid email or password.");

            // 4. Première connexion : générer une clé secrète pour 2FA et retourner le QR code
            if (string.IsNullOrEmpty(user.SecretKeyHash))
            {
                var secretKey = totpService.GenerateSecretKey();
                var qrCodeUri = totpService.GenerateQrCodeUri(secretKey, user.Email);

                user.SecretKeyHash = secretKey;

                await userRepository.UpdateAsync(user);

                return new LoginResponseDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role.ToString(),
                    IsFirstLogin = true,
                    TwoFactorRequired = true,
                    SecretKey = secretKey,
                    QrCodeUri = qrCodeUri
                };
            }

            // 5. Demander le code
            return new LoginResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role.ToString(),
                IsFirstLogin = false,
                TwoFactorRequired = true
            };

        }

        /// <summary>
        /// Cette méthode gère la logique d'inscription d'un nouvel utilisateur.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Le nouvel utilisateur créé ou null en cas d'échec</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<User?> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            // Vérifier si l'utilisateur existe déjà
            var existingUser = await userRepository.GetByEmailAsync(email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            // Hasher le mot de passe
            var passwordHash = passwordService.HashPassword(password);

            // Créer un nouvel utilisateur
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = passwordHash,
                SecretKeyHash = null,
                Role = Role.User,
                Status = Status.Active,
                CreatedAt = DateTime.UtcNow
            };

            // Enregistrer l'utilisateur dans la base de données
            return await userRepository.AddAsync(newUser);
        }
    }
}