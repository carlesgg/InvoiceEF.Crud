using System.Threading;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Domain.Contracts;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class CompanyService(ICompanyRepository companyRepository) : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;

        public async Task<IEnumerable<Company>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _companyRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Company?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Company company, CancellationToken cancellationToken)
        {
            return await _companyRepository.UpdateAsync(company, cancellationToken);
        }

        public async Task<bool> AddAsync(Company company, CancellationToken cancellationToken)
        {
            return await _companyRepository.AddAsync(company, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await _companyRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
