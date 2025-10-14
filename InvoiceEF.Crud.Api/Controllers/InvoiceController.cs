using Microsoft.AspNetCore.Mvc;
using System.Threading;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(IInvoiceService invoiceService) : ControllerBase
    {
        private readonly IInvoiceService _invoiceService = invoiceService;

        // GET: api/Invoice
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _invoiceService.GetInvoices(cancellationToken);

            if (result.HasErrors)
                return BadRequest(result.Errors);

            return Ok(result.Result);
        }

        // GET: api/Invoice/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _invoiceService.GetInvoiceById(id, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok(result.Result);
        }

        // POST: api/Invoice
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Invoice invoice, CancellationToken cancellationToken)
        {
            if (invoice == null)
                return BadRequest("Invoice cannot be null.");

            var result = await _invoiceService.AddInvoice(invoice, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Result.InvoiceId }, result.Result);
        }

        // PUT: api/Invoice/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Invoice invoice, CancellationToken cancellationToken)
        {
            if (id != invoice.InvoiceId)
                return BadRequest("ID mismatch.");

            var result = await _invoiceService.UpdateInvoice(invoice, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok(result.Result);
        }

        // DELETE: api/Invoice/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _invoiceService.DeleteInvoice(id, cancellationToken);

            if (result.HasErrors || !result.Result)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok();
        }
    }
}
