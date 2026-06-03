using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Core.Interfaces.Services.Auth;
using GBP.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GBP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        IAuthService authService) : ControllerBase
    {

        /// <summary>
        /// Fonction de connexion pour les utilisateurs. 
        /// Elle prend en entrée un objet LoginRequestDto contenant l'email et le mot de passe de l'utilisateur, 
        /// et retourne un objet LoginResponseDto contenant les informations de l'utilisateur 
        /// ainsi qu'un token d'accès si la connexion est réussie.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var reponse = await authService.LoginAsync(request.Email, request.Password);

                return Ok(reponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
<<<<<<< HEAD
        /// Fonction d'inscription pour les nouveaux utilisateurs.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Le nouvel utilisateur créé ou null en cas d'échec</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            try
            {
                User? user = await authService.RegisterAsync(
                    request.FirstName, request.LastName, request.Email, request.Password);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred during registration.", details = ex.Message });
            }
        }

        /// <summary>
=======
>>>>>>> cd5a8a7e7e6f91cd650125a16ede1543b8dc2cf0
        /// Vérifie le code de vérification à deux facteurs pour un utilisateur donné.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("2fa/verify")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> VerifyTwofactor([FromBody] TwoFactorRequestDto request)
        {
            try
            {
                var response = await authService.VerifyTwofactorAsync(request.Email, request.Code);

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

    }
}
