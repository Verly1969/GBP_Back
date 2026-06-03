using GBP.Core.DTOs.Response;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Auth
{
    public interface IAuthService
    {
<<<<<<< HEAD
        /// <summary>
        /// Cette méthode gère le processus de connexion pour un utilisateur donné.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Le DTO de réponse pour la connexion</returns>
        Task<LoginResponseDto> LoginAsync(string email, string password);

        /// <summary>
        /// Cette méthode gère la vérification du code 2FA pour un utilisateur donné.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns>Le DTO de réponse pour la connexion</returns>
        Task<LoginResponseDto> VerifyTwofactorAsync(string email, string code);

        /// <summary>
        /// Cette méthode gère le processus d'inscription pour un nouvel utilisateur.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Le nouvel utilisateur créé ou null en cas d'échec</returns>
        Task<User?> RegisterAsync(string firstName, string lastName, string email, string password);
=======
        Task<LoginResponseDto> LoginAsync(string email, string password);
        Task<LoginResponseDto> VerifyTwofactorAsync(string email, string code);
>>>>>>> cd5a8a7e7e6f91cd650125a16ede1543b8dc2cf0
    }
}
