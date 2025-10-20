using Microsoft.AspNetCore.Mvc;
using System.Threading;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(ICompanyService companyService) : BaseController
    {
        private readonly ICompanyService _companyService = companyService;

        // GET: api/Company
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _companyService.GetCompanies(cancellationToken);

            if (result.HasErrors)
                return BadRequest(result.Errors);

            return Ok(result.Result);
        }

        // GET: api/Company/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _companyService.GetCompanyById(id, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok(result.Result);
        }

        // POST: api/Company
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyEntity company, CancellationToken cancellationToken)
        {
            if (company == null)
                return BadRequest("Company cannot be null.");

            var result = await _companyService.AddCompany(company, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Result.CompanyId }, result.Result);
        }

        // PUT: api/Company/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CompanyEntity company, CancellationToken cancellationToken)
        {
            if (id != company.CompanyId)
                return BadRequest("ID mismatch.");

            var result = await _companyService.UpdateCompany(company, cancellationToken);

            if (result.HasErrors || result.Result == null)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok(result.Result);
        }

        // DELETE: api/Company/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _companyService.DeleteCompany(id, cancellationToken);

            if (result.HasErrors || !result.Result)
                return NotFound(result.Errors.Count > 0 ? result.Errors : null);

            return Ok();
        }
    }
}
