namespace GBP.API.DTOs.Response
{
    public class LoginResponseDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
