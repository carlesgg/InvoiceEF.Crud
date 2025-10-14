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

        // GET: api/<InvoiceLineController>
        [HttpGet]
        public async Task<IEnumerable<InvoiceLine>> Get(CancellationToken cancellationToken)
        {
            return await _invoiceLineService.GetAllAsync(cancellationToken);
        }

        // GET api/<InvoiceLineController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var invoiceLine = await _invoiceLineService.GetByIdAsync(id, cancellationToken);
            if (invoiceLine == null)
                return NotFound();

            return Ok(invoiceLine);
        }

        // POST api/<InvoiceLineController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InvoiceLine invoiceLine, CancellationToken cancellationToken)
        {
            bool result = await _invoiceLineService.AddAsync(invoiceLine, cancellationToken);

            if (result)
                return StatusCode(201); // 201 Created

            return BadRequest("Failed to create invoice line.");
        }

        // PUT api/<InvoiceLineController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] InvoiceLine invoiceLine, CancellationToken cancellationToken)
        {
            if (id != invoiceLine.LineId)
                return BadRequest("ID mismatch");

            bool updated = await _invoiceLineService.UpdateAsync(invoiceLine, cancellationToken);

            if (updated)
                return Ok();
            else
                return NotFound();
        }

        // DELETE api/<InvoiceLineController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            bool result = await _invoiceLineService.DeleteAsync(id, cancellationToken);
            if (result)
                return Ok();
            else
                return NotFound();
        }
    }
}
