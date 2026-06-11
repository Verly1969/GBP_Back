using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GBP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountTypeController(IAccountTypeService _accountTypeService) : ControllerBase
    {
        /// <summary>
        /// Récupère tous les types de comptes disponibles.
        /// </summary>
        /// <returns>Une liste de tous les types de comptes disponibles.</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountTypeResponseDto>), StatusCode = 200)]
        [ProducesResponseType(401, StatusCode = 401, Description = "Non autorisé")]
        [ProducesResponseType(500, StatusCode = 500, Description = "Erreur interne du serveur")]
        public async Task<IActionResult> GetAll()
        {
            var accountTypes = await _accountTypeService.GetAllAsync();
            return Ok(accountTypes);
        }


        /// <summary>
        /// Récupère un type de compte spécifique en fonction de son ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Le type de compte correspondant à l'ID spécifié ou null si non trouvé.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(AccountTypeResponseDto), StatusCode = 200)]
        [ProducesResponseType(404, StatusCode = 404, Description = "Type de compte non trouvé")]
        [ProducesResponseType(401, StatusCode = 401, Description = "Non autorisé")]
        [ProducesResponseType(500, StatusCode = 500, Description = "Erreur interne du serveur")]
        public async Task<IActionResult> GetById(int id)
        {
            var accountType = await _accountTypeService.GetByIdAsync(id);

            if (accountType == null)
            {
                return NotFound();
            }
            return Ok(accountType);
        }

        /// <summary>
        /// Crée un nouveau type de compte en fonction des données fournies dans le corps de la requête.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Le type de compte créé.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201, Type = typeof(AccountTypeResponseDto), StatusCode = 201)]
        [ProducesResponseType(400, StatusCode = 400, Description = "Données invalides")]
        [ProducesResponseType(401, StatusCode = 401, Description = "Non autorisé")]
        [ProducesResponseType(500, StatusCode = 500, Description = "Erreur interne du serveur")]
        public async Task<IActionResult> Create([FromBody] AccountTypesRequestDto request)
        {
            var accountType = await _accountTypeService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById), // ActionName
                new { id = accountType.Id }, accountType); // RouteValues
        }

        /// <summary>
        /// Met à jour un type de compte existant en fonction de son ID 
        /// et des données fournies dans le corps de la requête.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Le type de compte mis à jour ou une erreur si non trouvé.</returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(AccountTypeResponseDto), StatusCode = 200)]
        [ProducesResponseType(400, StatusCode = 400, Description = "Données invalides")]
        [ProducesResponseType(404, StatusCode = 404, Description = "Type de compte non trouvé")]
        [ProducesResponseType(401, StatusCode = 401, Description = "Non autorisé")]
        [ProducesResponseType(500, StatusCode = 500, Description = "Erreur interne du serveur")]
        public async Task<IActionResult> Update(int id, [FromBody] AccountTypesRequestDto request)
        {
            try
            {
                var updated = await _accountTypeService.UpdateAsync(id, request);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound( new { Message = "Type de compte non trouvé" });
            }
            catch (ArgumentException)
            {
                return BadRequest( new { Message = "Données invalides" });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204, StatusCode = 204)]
        [ProducesResponseType(404, StatusCode = 404, Description = "Type de compte non trouvé")]
        [ProducesResponseType(401, StatusCode = 401, Description = "Non autorisé")]
        [ProducesResponseType(500, StatusCode = 500, Description = "Erreur interne du serveur")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _accountTypeService.DeleteAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Type de compte non trouvé" });
            }
        }
    }
}
