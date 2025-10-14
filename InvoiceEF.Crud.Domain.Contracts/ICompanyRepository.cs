using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Domain.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync(CancellationToken cancellationToken);
        Task<Company?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(Company company, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Company company, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
