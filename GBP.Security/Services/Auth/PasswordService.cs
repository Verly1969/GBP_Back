using GBP.Core.Interfaces.Services.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Services.Auth
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            throw new NotImplementedException();
        }
    }

    // Paramètres de configuration Argon2id
    public record Argon2Params(
        int HashLength,   // longueur du hash en octets
        int SaltLength,   // longueur du sel en octets
        int MemoryCost, // en Ko
        int TimeCost,   // nombre d'itérations
        int Parallelism // nombre de threads
    );
}
