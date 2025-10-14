using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using System.Threading;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class CompanyService(ICompanyRepository companyRepository) : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;

        public async Task<OperationResult<IEnumerable<Company>>> GetCompanies(CancellationToken cancellationToken)
        {
            return await _companyRepository.GetCompanies(cancellationToken);
        }

        public async Task<OperationResult<Company?>> GetCompanyById(Guid id, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetCompanyById(id, cancellationToken);
        }

        public async Task<OperationResult<Company>> AddCompany(Company company, CancellationToken cancellationToken)
        {
            return await _companyRepository.AddCompany(company, cancellationToken);
        }

        public async Task<OperationResult<Company>> UpdateCompany(Company company, CancellationToken cancellationToken)
        {
            return await _companyRepository.UpdateCompany(company, cancellationToken);
        }

        public async Task<OperationResult<bool>> DeleteCompany(Guid id, CancellationToken cancellationToken)
        {
            return await _companyRepository.DeleteCompany(id, cancellationToken);
        }
    }
}
