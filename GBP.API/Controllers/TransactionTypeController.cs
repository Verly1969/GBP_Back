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
    public class TransactionTypeController(
        ITransactionTypeService _transactionTypeService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionTypeResponseDto>), Description = "Succès.")]
        public async Task<IActionResult> GetAll()
        {
            var transactionTypes = await _transactionTypeService.GetAllAsync();

            return Ok(transactionTypes);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionTypeResponseDto>), Description = "Succès.")]
        [ProducesResponseType(404, Description = "Le type de transaction n'a pas été trouvé.")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var transactionType = await _transactionTypeService.GetByIdAsync(id);
                return Ok(transactionType);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Description = "Type de transaction créé avec succès.")]
        public async Task<IActionResult> Create([FromBody] TransactionTypeRequestDto request)
        {
            var created = await _transactionTypeService.AddAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200, Description = "Le type de transaction à été modifiée avec succès.")]
        [ProducesResponseType(404, Description = "Le type de transaction n'a pas été trouvé.")]
        public async Task<IActionResult> Update(int id, TransactionTypeRequestDto request)
        {
            try
            {
                var updated = await _transactionTypeService.UpdateAsync(id, request);
                return Ok(updated);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204, Description = "Le type de transaction à été supprimé avec succès.")]
        [ProducesResponseType(404, Description = "Le type de transaction n'a pas été trouvé.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _transactionTypeService.DeleteAsync(id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
