using GBP.Core.DTOs.Request;
using GBP.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GBP.API.Controllers
{
    /// <summary>
    /// Contrôleur HTTP responsable des opérations CRUD sur les catégories.
    /// </summary>
    /// <remarks>
    /// Route: <c>api/category</c>. Ce contrôleur est protégé par authentification grâce à l'attribut <see cref="Authorize"/>.
    /// Il délègue la logique métier à <c>ICategoryService</c> injecté via le constructeur.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController(
        ICategoryService _categoryService) : ControllerBase
    {
        /// <summary>
        /// Récupère toutes les catégories disponibles.
        /// </summary>
        /// <remarks>
        /// Appelle le service des catégories via <c>_categoryService.GetAllAsync()</c> pour obtenir
        /// l'ensemble des enregistrements et renvoie le résultat au client.
        /// </remarks>
        /// <returns>
        /// Un <see cref="IActionResult"/> contenant un code HTTP 200 (OK) et la collection de catégories.
        /// </returns>
        /// <response code="200">La liste des catégories a été récupérée avec succès.</response>
        [HttpGet]
        [ProducesResponseType(200, Description = "La liste des catégories a été récupérée avec succès.")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        /// <summary>
        /// Récupère une catégorie par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la catégorie à récupérer.</param>
        /// <returns>
        /// Un <see cref="IActionResult"/> contenant la catégorie demandée.
        /// </returns>
        /// <response code="200">La catégorie a été trouvée et renvoyée.</response>
        /// <response code="404">Aucune catégorie ne correspond à l'identifiant fourni.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Description = "La catégorie a été trouvée et renvoyée.")]
        [ProducesResponseType(404, Description = "Aucune catégorie ne correspond à l'identifiant fourni.")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);

                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

        /// <summary>
        /// Crée une nouvelle catégorie.
        /// </summary>
        /// <param name="request">Données de la catégorie à créer.</param>
        /// <returns>
        /// Un <see cref="IActionResult"/> contenant la ressource créée et l'emplacement via l'en-tête Location.
        /// </returns>
        /// <response code="201">La catégorie a été créée avec succès.</response>
        /// <response code="400">La requête est invalide (données manquantes ou non valides).</response>
        [HttpPost]
        [ProducesResponseType(201, Description = "La catégorie a été créée avec succès.")]
        [ProducesResponseType(400, Description = "La requête est invalide (données manquantes ou non valides).")]
        public async Task<IActionResult> Create([FromBody] CategoryRequestDto request)
        {
            var category = await _categoryService.AddAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                category);
        }

        /// <summary>
        /// Met à jour une catégorie existante.
        /// </summary>
        /// <param name="id">Identifiant de la catégorie à mettre à jour.</param>
        /// <param name="request">Données utilisées pour la mise à jour.</param>
        /// <returns>
        /// Un <see cref="IActionResult"/> contenant la catégorie mise à jour.
        /// </returns>
        /// <response code="200">La catégorie a été mise à jour avec succès.</response>
        /// <response code="404">Aucune catégorie ne correspond à l'identifiant fourni.</response>
        /// <response code="400">La requête est invalide.</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200, Description = "La catégorie a été mise à jour avec succès.")]
        [ProducesResponseType(400, Description = "La requête est invalide.")]
        [ProducesResponseType(404, Description = "Aucune catégorie ne correspond à l'identifiant fourni.")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequestDto request)
        {
            try
            {
                var updated = await _categoryService.UpdateAsync(id, request);

                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

        /// <summary>
        /// Supprime une catégorie par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la catégorie à supprimer.</param>
        /// <returns>
        /// Un <see cref="IActionResult"/> indiquant le résultat de l'opération.
        /// </returns>
        /// <response code="204">La catégorie a été supprimée avec succès (aucun contenu).</response>
        /// <response code="404">Aucune catégorie ne correspond à l'identifiant fourni.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204, Description = "La catégorie a été supprimée avec succès (aucun contenu).")]
        [ProducesResponseType(404, Description = "Aucune catégorie ne correspond à l'identifiant fourni.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
    }
