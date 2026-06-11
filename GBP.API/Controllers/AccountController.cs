using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GBP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController(
        IAccountService _accountService) : ControllerBase
    {
        // Helper pour récupérer l'ID de l'utilisateur connecté à partir du token JWT
        private Guid GetUserId() =>
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value
                ?? throw new UnauthorizedAccessException("Utilisateur non authentifié"));

        // Helper pour récupérer l'email de l'utilisateur connecté à partir du token JWT
        private string GetUserEmail() =>
            User.FindFirst(ClaimTypes.Email)?.Value
                ?? User.FindFirst("email")?.Value
                ?? throw new UnauthorizedAccessException("Email non trouvé dans le token");

        /// <summary>
        /// Récupère tous les comptes associés à l'utilisateur connecté.
        /// </summary>
        /// <returns>Une liste de comptes</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountResponseDto>), StatusCode = StatusCodes.Status200OK)]
        [ProducesResponseType(401, StatusCode = StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(500, StatusCode = StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var accounts = await _accountService.GetAllByUserIdAsync(userId);
            return Ok(accounts);
        }

        /// <summary>
        /// Récupère les détails d'un compte spécifique par son ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Les détails du compte ou une erreur 404 si non trouvé</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(AccountResponseDto), StatusCode = StatusCodes.Status200OK)]
        [ProducesResponseType(404, StatusCode = StatusCodes.Status404NotFound)]
        [ProducesResponseType(401, StatusCode = StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(500, StatusCode = StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (account is null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        /// <summary>
        /// Crée un nouveau compte pour l'utilisateur connecté.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Le compte créé</returns>
        [HttpPost]
        [ProducesResponseType(201, StatusCode = StatusCodes.Status201Created)]
        [ProducesResponseType(401, StatusCode = StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(500, StatusCode = StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AccountRequestDto request)
        {
            var userId = GetUserId();
            var account = await _accountService.CreateAsync(request, userId);
            return CreatedAtAction(
                nameof(GetById), 
                new { id = account.Id },
                account);
        }

        /// <summary>
        /// Met à jour les informations d'un compte existant. 
        /// Seul le propriétaire du compte peut effectuer cette opération.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Le compte mis à jour</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(200, StatusCode = StatusCodes.Status200OK)]
        [ProducesResponseType(400, StatusCode = StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404, StatusCode = StatusCodes.Status404NotFound)]
        [ProducesResponseType(401, StatusCode = StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(500, StatusCode = StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] AccountRequestDto request)
        {
            try
            {
                var email = GetUserEmail();
                var updatedAccount = await _accountService.UpdateAsync(id, request, email);
                return Ok(updatedAccount);
            }
            catch (KeyNotFoundException)
            {
                return NotFound( new { Message = "Compte non trouvé" });
            }
            catch (ArgumentException) 
            {
                return BadRequest( new { Message = "Requête invalide" });
            }
        }

        /// <summary>
        /// Supprime un compte existant. Seul le propriétaire du compte peut effectuer cette opération.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200, StatusCode = StatusCodes.Status200OK)]
        [ProducesResponseType(404, StatusCode = StatusCodes.Status404NotFound)]
        [ProducesResponseType(401, StatusCode = StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(500, StatusCode = StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _accountService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound( new { Message = "Compte non trouvé" });
            }
        }
    }
}
