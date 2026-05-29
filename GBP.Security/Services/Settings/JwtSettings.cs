using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Services.Settings
{
    public class JwtSettings
    {
        public required string SecretKey { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required int ExpiryMinutes { get; set; }
    }
}
