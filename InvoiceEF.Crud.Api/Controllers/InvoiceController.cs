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

        // GET: api/<InvoiceController>
        [HttpGet]
        public async Task<IEnumerable<Invoice>> Get(CancellationToken cancellationToken)
        {
            return await _invoiceService.GetAllAsync(cancellationToken);
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceService.GetByIdAsync(id, cancellationToken);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        // POST api/<InvoiceController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Invoice invoice, CancellationToken cancellationToken)
        {
            bool result = await _invoiceService.AddAsync(invoice, cancellationToken);

            if (result)
                return StatusCode(201); // 201 Created

            return BadRequest("Failed to create invoice.");
        }

        // PUT api/<InvoiceController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Invoice invoice, CancellationToken cancellationToken)
        {
            if (id != invoice.InvoiceId)
                return BadRequest("ID mismatch");

            bool updated = await _invoiceService.UpdateAsync(invoice, cancellationToken);

            if (updated)
                return Ok();
            else
                return NotFound();
        }

        // DELETE api/<InvoiceController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            bool result = await _invoiceService.DeleteAsync(id, cancellationToken);
            if (result)
                return Ok();
            else
                return NotFound();
        }
    }
}
