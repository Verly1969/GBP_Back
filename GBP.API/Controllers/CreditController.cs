using GBP.Core.DTOs.Request;
using GBP.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace GBP.API.Controllers
{
    [Route("api/account/{accountId:guid}/credit")]
    [ApiController]
    [Authorize]
    public class CreditController(
        ICreditService _creditService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllByAccountId(Guid accountId)
        {
            var credits = await _creditService.GetAllByAccountIdAsync(accountId);
            return Ok(credits);
        }

        [HttpGet("{creditId:guid}")]
        public async Task<IActionResult> GetById(Guid accountId, Guid creditId)
        {
            var credit = await _creditService.GetByIdAsync(creditId);

            return credit is null ? NotFound() : Ok(credit);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid accountId, [FromBody] CreditRequestDto request)
        {
            var created = await _creditService.CreateAsync(request, accountId);

            return CreatedAtAction(
                nameof(GetById),
                new { accountId, id = created.Id },
                created);
        }

        [HttpPut("{creditId:guid}")]
        public async Task<IActionResult> Update(Guid accountId, Guid creditId, [FromBody] CreditRequestDto request)
        {
            try
            {
                var updated = await _creditService.UpdateAsync(creditId, request);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound( new { Message = $"Le credit '{creditId} non trouvé" } );
            }
            catch (InvalidOperationException)
            {
                return BadRequest( new { Message = "Données invalides" } );
            }
        }

        [HttpDelete("{creditId:guid}")]
        public async Task<IActionResult> Delete(Guid accountId, Guid creditId)
        {
            try
            {
                var deleted = await _creditService.DeleteAsync(creditId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound( new { Message = $"Le crédit {creditId} non trouvé" } );
            }
        }
    }
}
