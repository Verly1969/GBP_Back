namespace GBP.Core.DTOs.Response
{
    public class LoginResponseDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public string? AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public bool IsFirstLogin { get; set; }
        public bool TwoFactorRequired { get; set; }
        public string? SecretKey { get; set; }
        public string? QrCodeUri { get; set; }
    }
}
