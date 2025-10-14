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

        // GET: api/<ClientController> (GET ALL)
        [HttpGet]
        public async Task<IEnumerable<Client>> Get(CancellationToken cancellationToken)
        {
            return await _clientService.GetAllAsync(cancellationToken);
        }

        // GET api/<ClientController>/5 (GET BY ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var client = await _clientService.GetByIdAsync(id, cancellationToken);
            if (client == null)
                return NotFound();

            return Ok(client);
        }


        // POST api/<ClientController> (CREATE)
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Client client, CancellationToken cancellationToken)
        {
            bool result = await _clientService.AddAsync(client, cancellationToken);

            if (result)
                return StatusCode(201); // 201 Created

            return BadRequest("Failed to create client.");
        }


        // PUT api/<ClientController>/5 (UPDATE)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Client client, CancellationToken cancellationToken)
        {
            if (id != client.ClientId)
                return BadRequest("ID mismatch");

            bool updated = await _clientService.UpdateAsync(client, cancellationToken);

            if (updated)
                return Ok();
            else
                return NotFound();
        }


        // DELETE api/<ClientController>/5 (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            bool result = await _clientService.DeleteAsync(id, cancellationToken);
            if (result)
                return Ok();  // 200 OK If successfully deleted
            else
                return NotFound();  // 404 If the client was not found
        }

    }
}
