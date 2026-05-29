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
