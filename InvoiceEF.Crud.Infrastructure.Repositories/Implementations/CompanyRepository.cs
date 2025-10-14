using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using Microsoft.EntityFrameworkCore;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class CompanyRepository(AppDbContext context) : ICompanyRepository
    {
        private readonly AppDbContext _context = context;

        // Get all companies
        public async Task<OperationResult<IEnumerable<Company>>> GetCompanies(CancellationToken cancellationToken)
        {
            var companies = await _context.Companies.ToListAsync(cancellationToken);
            return new OperationResult<IEnumerable<Company>>().AddResult(companies);
        }

        // Get company by Id
        public async Task<OperationResult<Company?>> GetCompanyById(Guid id, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.FindAsync([id], cancellationToken);
            return new OperationResult<Company?>().AddResult(company);
        }

        // Add company
        public async Task<OperationResult<Company>> AddCompany(Company company, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Company>();

            try
            {
                await _context.Companies.AddAsync(company, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                result.AddResult(company);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }

            return result;
        }

        // Update company
        public async Task<OperationResult<Company>> UpdateCompany(Company company, CancellationToken cancellationToken)
        {
            // Fetch existing company first
            var existingResult = await GetCompanyById(company.CompanyId, cancellationToken);

            if (existingResult.Result == null)
            {
                return new OperationResult<Company>().AddError(404, "Company not found");
            }

            var result = new OperationResult<Company>();
            try
            {
                _context.Companies.Update(company);
                await _context.SaveChangesAsync(cancellationToken);
                result.AddResult(company);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }

            return result;
        }

        // Delete company
        public async Task<OperationResult<bool>> DeleteCompany(Guid id, CancellationToken cancellationToken)
        {
            // Fetch existing company first
            var existingResult = await GetCompanyById(id, cancellationToken);

            if (existingResult.Result == null)
            {
                return new OperationResult<bool>().AddError(404, "Company not found");
            }

            var result = new OperationResult<bool>();
            try
            {
                _context.Companies.Remove(existingResult.Result);
                await _context.SaveChangesAsync(cancellationToken);
                result.AddResult(true);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }

            return result;
        }
    }
}
