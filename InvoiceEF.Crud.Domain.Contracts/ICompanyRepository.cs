using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Domain.Contracts
{
    public interface ICompanyRepository
    {
        Task<OperationResult<IEnumerable<CompanyEntity>>> GetCompanies(CancellationToken cancellationToken);
        Task<OperationResult<CompanyEntity?>> GetCompanyById(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<CompanyEntity>> AddCompany(CompanyEntity company, CancellationToken cancellationToken);
        Task<OperationResult<CompanyEntity>> UpdateCompany(CompanyEntity company, CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteCompany(Guid id, CancellationToken cancellationToken);
    }
}
