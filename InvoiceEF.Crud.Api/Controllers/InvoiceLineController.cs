using Microsoft.AspNetCore.Mvc;
using System.Threading;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceLineController(IInvoiceLineService invoiceLineService) : ControllerBase
    {
        private readonly IInvoiceLineService _invoiceLineService = invoiceLineService;

        // GET: api/InvoiceLine
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _invoiceLineService.GetInvoiceLines(cancellationToken);

            if (result.HasErrors)
                return BadRequest(result.Errors);

            return Ok(result.Result);
        }

        // GET: api/InvoiceLine/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _invoiceLineService.GetInvoiceLineById(id, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok(result.Result);
        }

        // POST: api/InvoiceLine
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InvoiceLineEntity invoiceLine, CancellationToken cancellationToken)
        {
            if (invoiceLine == null)
                return BadRequest("InvoiceLine cannot be null.");

            var result = await _invoiceLineService.AddInvoiceLine(invoiceLine, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Result.LineId }, result.Result);
        }

        // PUT: api/InvoiceLine/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] InvoiceLineEntity invoiceLine, CancellationToken cancellationToken)
        {
            if (id != invoiceLine.LineId)
                return BadRequest("ID mismatch.");

            var result = await _invoiceLineService.UpdateInvoiceLine(invoiceLine, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok(result.Result);
        }

        // DELETE: api/InvoiceLine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _invoiceLineService.DeleteInvoiceLine(id, cancellationToken);

            if (result.HasErrors || !result.Result)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok();
        }
    }
}
