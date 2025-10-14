using Microsoft.AspNetCore.Mvc;
using System.Threading;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(ICompanyService companyService) : ControllerBase
    {
        private readonly ICompanyService _companyService = companyService;

        // GET: api/<CompanyController>
        [HttpGet]
        public async Task<IEnumerable<Company>> Get(CancellationToken cancellationToken)
        {
            return await _companyService.GetAllAsync(cancellationToken);
        }

        // GET api/<CompanyController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var company = await _companyService.GetByIdAsync(id, cancellationToken);
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        // POST api/<CompanyController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company company, CancellationToken cancellationToken)
        {
            bool result = await _companyService.AddAsync(company, cancellationToken);

            if (result)
                return StatusCode(201); // 201 Created

            return BadRequest("Failed to create company.");
        }

        // PUT api/<CompanyController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Company company, CancellationToken cancellationToken)
        {
            if (id != company.CompanyId)
                return BadRequest("ID mismatch");

            bool updated = await _companyService.UpdateAsync(company, cancellationToken);

            if (updated)
                return Ok();
            else
                return NotFound();
        }

        // DELETE api/<CompanyController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            bool result = await _companyService.DeleteAsync(id, cancellationToken);
            if (result)
                return Ok();
            else
                return NotFound();
        }
    }
}
