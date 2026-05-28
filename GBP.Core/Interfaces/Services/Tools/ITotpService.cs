using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Tools
{
    public interface ITotpService
    {
        // Génère une clé secrète pour l'utilisateur
        string GenerateSecretKey();

        // Génére l'URI du QR code à scanner avec une application d'authentification (ex: Google Authenticator)
        string GenerateQrCodeUri(string secretKey, string userEmail);

        // Vérifie le code TOTP fourni par l'utilisateur
        bool ValidateCode(string secretKey, string code);

    }
}
