using GBP.Core.DTOs.Request;
using GBP.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GBP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreditTypeController(ICreditTypeService _creditTypeService) : ControllerBase
    {
        /// <summary>
        /// Récupère tous les types de crédits disponibles.
        /// </summary>
        /// <returns>Une liste de tous les types de crédits disponibles.</returns>
        [HttpGet]
        [ProducesResponseType(200, Description = "Liste de crédits trouvée avec succès")]
        [ProducesResponseType(500, Description = "Erreur serveur")]
        public async Task<IActionResult> GetAll()
        {
            var creditTypes = await _creditTypeService.GetAllAsync();

            return Ok(creditTypes);
        }

        /// <summary>
        /// Récupère un type de crédit spécifique en fonction de son ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Le type de crédit correspondant à l'ID spécifié ou null si non trouvé.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Description = "Type de crédit trouvé avec succès")]
        [ProducesResponseType(404, Description = "Type de crédit non trouvé")]
        [ProducesResponseType(500, Description = "Erreur serveur")]
        public async Task<IActionResult> GetById(int id)
        {
            var creditType = await _creditTypeService.GetById(id);

            return creditType is null ? NotFound() : Ok(creditType);
        }

        /// <summary>
        /// Crée un nouveau type de crédit en fonction des données fournies dans le corps de la requête.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Le type de crédit créé.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201, Description = "Nouveau type de crédit créé.")]
        [ProducesResponseType(500, Description = "Erreur serveur")]
        public async Task<IActionResult> CreateAsync([FromBody] CreditTypeRequestDto request)
        {
            var created = await _creditTypeService.CreateAsync(request);

            return CreatedAtAction( // retourne un 201 avec header location qui pointe sur l'url
                nameof(GetById), // nom de l'action à appeler
                new { id = created.Id }, // paramètre de l'action
                created); // corps de la réponse
        }

        /// <summary>
        /// Met à jour un type de crédit existant en fonction de son ID 
        /// et des données fournies dans le corps de la requête.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Le type de crédit mis à jour ou une erreur si non trouvé.</returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Description = "Type de crédit mis à jour avec succès")]
        [ProducesResponseType(400, Description = "Données invalides")]
        [ProducesResponseType(404, Description = "Type de crédit non trouvé")]
        [ProducesResponseType(500, Description = "Erreur serveur")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CreditTypeRequestDto request)
        {
            try
            {
                var updated = await _creditTypeService.UpdateAsync(id, request);

                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound( new { message = "Type de crédit non trouvé" } );
            }
            catch (ArgumentException)
            {
                return BadRequest(new { message = "Données invalides" });
            }
        }

        /// <summary>
        /// Supprime un type de crédit existant en fonction de son ID 
        /// et des données fournies dans le corps de la requête.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Ok ou une erreur si non trouvé.</returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204, Description = "Type de crédit supprimé avec succès")]
        [ProducesResponseType(404, Description = "Type de crédit non trouvé")]
        [ProducesResponseType(500, Description = "Erreur serveur")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var deleted = await _creditTypeService.DeleteAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Type de crédit non trouvé" });
            }
        }
    }
}
