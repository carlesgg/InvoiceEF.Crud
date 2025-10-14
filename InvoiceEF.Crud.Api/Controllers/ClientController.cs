using Microsoft.AspNetCore.Mvc;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Domain.Entities;


namespace InvoiceEF.Crud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IClientService clientService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;

        // GET: api/Client
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _clientService.GetClients(cancellationToken);

            if (result.HasErrors)
                return BadRequest(result.Errors);

            return Ok(result.Result);
        }

        // GET: api/Client/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _clientService.GetClientById(id, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok(result.Result);
        }

        // POST: api/Client
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Client client, CancellationToken cancellationToken)
        {
            if (client == null)
                return BadRequest("Client cannot be null.");

            var result = await _clientService.AddClient(client, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Result.ClientId }, result.Result);
        }

        // PUT: api/Client/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Client client, CancellationToken cancellationToken)
        {
            if (id != client.ClientId)
                return BadRequest("ID mismatch.");

            var result = await _clientService.UpdateClient(client, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok(result.Result);
        }

        // DELETE: api/Client/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _clientService.DeleteClient(id, cancellationToken);

            if (result.HasErrors || !result.Result)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok();
        }

    }
}
