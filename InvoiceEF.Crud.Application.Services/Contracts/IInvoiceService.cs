using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Application.Services.Contracts
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllAsync(CancellationToken cancellationToken);
        Task<Invoice?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(Invoice invoice, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Invoice invoice, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
