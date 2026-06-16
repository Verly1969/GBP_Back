using GBP.Core.DTOs.Request;
using GBP.Core.Interfaces.Services.Data;
using GBP.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace GBP.API.Controllers
{
    [Route("api/account/{accountId:guid}/credit")]
    [ApiController]
    [Authorize]
    public class CreditController(
        ICreditService _creditService) : ControllerBase
    {
        // Helper pour récupérer l'utilisateur courant
        private Guid GetUserId() =>
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value
                ?? throw new UnauthorizedAccessException(
                    "Utilisateur non identifié."));

        /// <summary>
        /// Récupère tous les crédits associés à un compte donné.
        /// </summary>
        /// <param name="accountId">L'identifiant du compte.</param>
        /// <returns>Une liste de crédits associés au compte.</returns>
        [HttpGet]
        [ProducesResponseType(200, Description = "Liste des crédits associés au compte.")]
        public async Task<IActionResult> GetAllByAccountId(Guid accountId)
        {
            var credits = await _creditService.GetAllByAccountIdAsync(accountId);
            return Ok(credits);
        }

        /// <summary>
        /// Récupère un crédit spécifique par son identifiant.
        /// </summary>
        /// <param name="accountId">L'identifiant du compte.</param>
        /// <param name="creditId">L'identifiant du crédit.</param>
        /// <returns>Le crédit correspondant ou une erreur 404 si non trouvé.</returns>
        [HttpGet("{creditId:guid}")]
        [ProducesResponseType(200, Description = "Crédit trouvé.")]
        [ProducesResponseType(404, Description = "Le crédit est introuvable.")]
        public async Task<IActionResult> GetById(Guid accountId, Guid creditId)
        {
            var credit = await _creditService.GetByIdAsync(creditId);

            return credit is null ? NotFound() : Ok(credit);
        }

        /// <summary>
        /// Crée un nouveau crédit pour un compte donné. 
        /// Seul le propriétaire du compte ou un administrateur peut effectuer cette action.
        /// </summary>
        /// <param name="accountId">L'identifiant du compte.</param>
        /// <param name="request">Les données du crédit à créer.</param>
        /// <returns>Le crédit créé ou une erreur si l'opération échoue.</returns>
        [HttpPost]
        [ProducesResponseType(404, Description = "Le compte est introuvable.")]
        [ProducesResponseType(401, Description = "Vous n'êtes pas autorisé à accéder à ce compte.")]
        [ProducesResponseType(400, Description = "Ce compte ne peut-être utilisé pour cette action.")]
        public async Task<IActionResult> Create(Guid accountId, [FromBody] CreditRequestDto request)
        {
            try
            {
                var userId = GetUserId();
                var created = await _creditService.CreateAsync(request, accountId, userId);

                return CreatedAtAction(
                    nameof(GetById),
                    new { accountId, creditId = created.Id },
                    created);
            }
            catch(KeyNotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch(UnauthorizedAccessException e)
            {
                return Unauthorized(new { message = e.Message });
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(new { message = e.Message });
            }

        }

        /// <summary>
        /// Met à jour un crédit existant. Seul le propriétaire du compte ou un administrateur peut effectuer cette action.
        /// </summary>
        /// <param name="accountId">L'identifiant du compte.</param>
        /// <param name="creditId">L'identifiant du crédit.</param>
        /// <param name="request">Les données mises à jour du crédit.</param>
        /// <returns>Le crédit mis à jour ou une erreur si l'opération échoue.</returns>
        [HttpPut("{creditId:guid}")]
        [ProducesResponseType(404, Description = "Le crédit est introuvable.")]
        [ProducesResponseType(400, Description = "Données invalides")]
        public async Task<IActionResult> Update(Guid accountId, Guid creditId, [FromBody] CreditRequestDto request)
        {
            try
            {
                var updated = await _creditService.UpdateAsync(creditId, request);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound( new { message = $"Le credit '{creditId}' non trouvé" } );
            }
            catch (InvalidOperationException)
            {
                return BadRequest( new { message = "Données invalides" } );
            }
        }

        /// <summary>
        /// Supprime un crédit existant. Seul le propriétaire du compte ou un administrateur peut effectuer cette action.
        /// </summary>
        /// <param name="accountId">L'identifiant du compte.</param>
        /// <param name="creditId">L'identifiant du crédit.</param>
        /// <returns>Une réponse indiquant le succès ou l'échec de l'opération.</returns>
        [HttpDelete("{creditId:guid}")]
        [ProducesResponseType(204, Description = "Crédit supprimé avec succès.")]
        [ProducesResponseType(404, Description = "Le crédit est introuvable.")]
        public async Task<IActionResult> Delete(Guid accountId, Guid creditId)
        {
            try
            {
                var deleted = await _creditService.DeleteAsync(creditId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound( new { message = $"Le crédit {creditId} non trouvé" } );
            }
        }
    }
}
