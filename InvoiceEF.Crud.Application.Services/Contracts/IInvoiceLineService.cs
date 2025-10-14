using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Application.Services.Contracts
{
    public interface IInvoiceLineService
    {
        Task<IEnumerable<InvoiceLine>> GetAllAsync(CancellationToken cancellationToken);
        Task<InvoiceLine?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(InvoiceLine invoiceLine, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(InvoiceLine invoiceLine, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
