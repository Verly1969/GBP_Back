using GBP.Core.DTOs.Request;
using GBP.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GBP.API.Controllers
{
    /// <summary>
    /// Controller pour gérer les opérations CRUD sur les sous-catégories
    /// liées à une catégorie donnée.
    /// Route: <c>api/category/{categoryId:int}/subcategory</c>.
    /// Requiert une authentification (__Authorize__).
    /// </summary>
    /// <remarks>
    /// Utilise <see cref="ISubCategoryService"/> pour déléguer la logique métier.
    /// </remarks>
    [Route("api/category/{categoryId:int}/subcategory")]
    [ApiController]
    [Authorize]
    public class SubCategoryController(
        ISubCategoryService _subCategoryService) : ControllerBase
    {
        /// <summary>
        /// Récupère toutes les sous-catégories associées à une catégorie.
        /// </summary>
        /// <param name="categoryId">Identifiant de la catégorie dont on souhaite les sous-catégories.</param>
        /// <returns>
        /// 200 OK avec la liste des sous-catégories. Le format retourné dépend du DTO renvoyé par le service.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Description = "Recherche effectuée avec succès.")]
        public async Task<IActionResult> GetAllByCategoryId(int categoryId)
        {
            var subCategory = await _subCategoryService.GetAllByCategoryIdAsync(categoryId);
            return Ok(subCategory);
        }

        /// <summary>
        /// Récupère une sous-catégorie par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la sous-catégorie.</param>
        /// <returns>
        /// 200 OK avec la sous-catégorie si trouvée ; 
        /// 404 Not Found si l'identifiant n'existe pas.
        /// </returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Description = "La sous-catégorie a été trouvée et renvoyée.")]
        [ProducesResponseType(404, Description = "Aucune sous-catégorie ne correspond à l'identifiant fourni.")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var subCategory = await _subCategoryService.GetByIdAsync(id);
                return Ok(subCategory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Crée une nouvelle sous-catégorie.
        /// </summary>
        /// <param name="request">DTO contenant les données de la sous-catégorie à créer.</param>
        /// <returns>
        /// 201 Created avec la ressource créée et l'en-tête Location pointant vers <see cref="GetById(int)"/>.
        /// 400 La requête est invalide
        /// </returns>
        [HttpPost]
        [ProducesResponseType(201, Description = "La sous-catégorie a été créée avec succès.")]
        [ProducesResponseType(400, Description = "La requête est invalide (données manquantes ou non valides).")]
        public async Task<IActionResult> Create([FromBody] SubCategoryRequestDto request)
        {
            var created = await _subCategoryService.AddAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        /// <summary>
        /// Met à jour une sous-catégorie existante.
        /// </summary>
        /// <param name="id">Identifiant de la sous-catégorie à mettre à jour.</param>
        /// <param name="request">DTO avec les nouvelles valeurs.</param>
        /// <returns>
        /// 200 OK avec la ressource mise à jour ; 
        /// 400 Bad Request requête invalide
        /// 404 Not Found si l'identifiant n'existe pas.
        /// </returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200, Description = "La sous-catégorie a été mise à jour avec succès.")]
        [ProducesResponseType(400, Description = "La requête est invalide.")]
        [ProducesResponseType(404, Description = "Aucune sous-catégorie ne correspond à l'identifiant fourni.")]
        public async Task<IActionResult> Update(int id, [FromBody] SubCategoryRequestDto request)
        {
            try
            {
                var updated = await _subCategoryService.UpdateAsync(id, request);
                return Ok(updated);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Supprime une sous-catégorie par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la sous-catégorie à supprimer.</param>
        /// <returns>
        /// 204 No Content si la suppression réussit ; 
        /// 404 Not Found si l'identifiant n'existe pas.
        /// </returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204, Description = "La sous-catégorie a été supprimée avec succès (aucun contenu).")]
        [ProducesResponseType(404, Description = "Aucune sous-catégorie ne correspond à l'identifiant fourni.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _subCategoryService.DeleteAsync(id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}