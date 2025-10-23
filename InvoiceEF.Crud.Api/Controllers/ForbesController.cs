using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Application.Services.Implementations;
using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceEF.Crud.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForbesController(IForbesService forbesService) : ControllerBase
    {
        private readonly IForbesService _forbesService = forbesService;

        [HttpGet("list")]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            var list = await _forbesService.GetBillionairesAsync(cancellationToken);
            return Ok(list);
        }

        // GET: api/Forbes/{rank}
        [HttpGet("{rank:int}")]
        public async Task<IActionResult> GetBillionaireByRank(int rank, CancellationToken cancellationToken)
        {
            var person = await _forbesService.GetBillionaireByRankAsync(rank, cancellationToken);
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        // POST: api/Forbes/save
        [HttpPost("save")]
        public async Task<IActionResult> SaveBillionaires([FromBody] ForbesPersonDto[] dtos, CancellationToken cancellationToken)
        {
            if (dtos == null || dtos.Length == 0)
                return BadRequest("No data provided.");

            await _forbesService.SaveBillionairesAsync(dtos, cancellationToken);
            return Ok(new { Message = "Saved successfully", Count = dtos.Length });
        }
    }
}
