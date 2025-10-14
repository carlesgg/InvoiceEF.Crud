using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Domain.Contracts
{
    public interface ICompanyRepository
    {
        Task<OperationResult<IEnumerable<Company>>> GetCompanies(CancellationToken cancellationToken);
        Task<OperationResult<Company?>> GetCompanyById(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<Company>> AddCompany(Company company, CancellationToken cancellationToken);
        Task<OperationResult<Company>> UpdateCompany(Company company, CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteCompany(Guid id, CancellationToken cancellationToken);
    }
}
