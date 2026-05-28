using GBP.Core.Interfaces.Services.Tools;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Services.Auth
{
    public class TotpService : ITotpService
    {
        private const string Issuer = "GBP";

        /// <summary>
        /// Génère l'URI du QR code à scanner avec une application d'authentification (ex: Google Authenticator).
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public string GenerateQrCodeUri(string secretKey, string userEmail)
        {
            return $"otpauth://totp/{Uri.EscapeDataString(Issuer)}"// L'émetteur (issuer) est généralement le nom de l'application ou du service "GBP")
                  + $":{Uri.EscapeDataString(userEmail)}"// L'identifiant de l'utilisateur (généralement son email)
                  + $"?secret={secretKey}"// La clé secrète générée pour l'utilisateur
                  + $"&issuer={Uri.EscapeDataString(Issuer)}"// L'émetteur (issuer) est généralement le nom de l'application ou du service "GBP")
                  + $"&algorithm=SHA256"// L'algorithme de hachage utilisé pour générer les codes TOTP (ex: SHA1, SHA256, SHA512)
                  + $"&digits=6"// Le nombre de chiffres dans les codes TOTP générés (ex: 6 ou 8)
                  + $"&period=30"// La période de validité des codes TOTP en secondes (ex: 30 secondes)
                  ;
        }

        /// <summary>
        /// Génère une clé secrète pour l'utilisateur. 
        /// Cette clé doit être stockée de manière sécurisée (ex: dans la base de données) 
        /// et utilisée pour générer les codes TOTP que l'utilisateur devra fournir 
        /// lors de l'authentification à deux facteurs.
        /// </summary>
        /// <returns></returns>
        public string GenerateSecretKey()
        {
            var key = KeyGeneration.GenerateRandomKey(20); // Génère une clé de 20 bytes (160 bits)
            return Base32Encoding.ToString(key);
        }

        public bool ValidateCode(string secretKey, string code)
        {
            try
            {
                var key = Base32Encoding.ToBytes(secretKey);// Convertit la clé secrète de sa représentation Base32 en bytes
                var totp = new Totp(key);// Crée une instance de Totp avec la clé secrète

                // Vérifie le code TOTP fourni par l'utilisateur en utilisant la méthode VerifyTotp de la
                // bibliothèque OtpNet
                return totp.VerifyTotp(code, out _);
            }
            catch
            {
                return false;
            }
        }
    }
}
