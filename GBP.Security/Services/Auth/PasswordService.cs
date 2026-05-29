using GBP.Core.Interfaces.Services.Auth;
using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GBP.Security.Services.Auth
{
    public class PasswordService : IPasswordService
    {
        /// <summary>
        /// Paramètres de configuration pour différentes versions d'Argon2id. 
        /// Actuellement, seule la version "v1" est définie, mais d'autres versions 
        /// peuvent être ajoutées à l'avenir avec des paramètres différents.
        /// </summary>
        private static readonly Dictionary<string, Argon2Params> Version = new()
        {
            ["v1"] = new Argon2Params(
                HashLength: 32,
                SaltLength: 16,
                MemoryCost: 65536, // 64 Mo
                TimeCost: 3,
                Parallelism: 4
            )
        };

        // Version par défaut utilisée si aucune version spécifique n'est fournie lors
        // de l'appel des méthodes de hachage ou de vérification.
        private const string DefaultVersion = "v1";

        // Méthode privée pour calculer le hash d'un mot de passe en utilisant les paramètres spécifiés.
        private static byte[] Compute(string password, byte[] salt, Argon2Params version)
        {
            // Implémentation du hachage Argon2id
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = version.Parallelism,
                Iterations = version.TimeCost,
                MemorySize = version.MemoryCost
            };

            // Retourne le hash calculé avec la longueur spécifiée dans les paramètres de la version.
            return argon2.GetBytes(version.HashLength);
        }

        // Méthode publique pour hacher un mot de passe.
        // Elle génère un sel aléatoire, calcule le hash
        public string HashPassword(string password)
        {
            var version = Version[DefaultVersion];
            var salt = RandomNumberGenerator.GetBytes(version.SaltLength);
            var hash = Compute(password, salt, version);

            // Le format de la chaîne de hachage est :
            // version.salt.hash,
            // où le sel et le hash sont encodés en Base64 pour faciliter le stockage et la transmission.
            return $"{DefaultVersion}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Méthode de vérification du mot de passe qui compare le mot de passe fourni avec le hash stocké.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        public bool VerifyPassword(string password, string hashedPassword)
        {
            Console.WriteLine($"Hash reçu : '{hashedPassword}'");

            var parts = hashedPassword.Split('.');

            Console.WriteLine($"Nombre de parties : {parts.Length}");
            Console.WriteLine($"Partie 0 : '{parts[0]}'");

            if (parts.Length != 3) return false;

            var version = parts[0];

            // Vérification de la version spécifiée dans la chaîne de hachage.
            if (!Version.TryGetValue(version, out var v)) return false;

            try
            {
                // Décodage du sel et du hash à partir de la chaîne de hachage.
                var salt = Convert.FromBase64String(parts[1]);
                Console.WriteLine($"Sel décodé : OK ({salt.Length} bytes)");

                // Le hash stocké est également décodé pour être comparé
                // avec le hash calculé à partir du mot de passe fourni.
                var hash = Convert.FromBase64String(parts[2]);
                Console.WriteLine($"Hash décodé : OK ({hash.Length} bytes)");

                // Calcul du hash du mot de passe fourni en utilisant le même sel
                // et les mêmes paramètres que ceux utilisés pour générer le hash stocké.
                var computedHash = Compute(password, salt, v);
                Console.WriteLine($"Hash calculé : OK ({computedHash.Length} bytes)");

                // Utilisation de CryptographicOperations.FixedTimeEquals pour éviter les attaques par timing.
                var result = CryptographicOperations.FixedTimeEquals(hash, computedHash);
                Console.WriteLine($"Résultat : {result}");

                return result;

            }
            catch (Exception ex)
            {
                // En cas d'erreur de format ou de conversion, la vérification échoue.
                Console.WriteLine($"EXCEPTION dans Verify : {ex.GetType().Name} — {ex.Message}");
                return false;
            }
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
