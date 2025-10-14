using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Domain.Contracts
{
    public interface IInvoiceRepository
    {
        Task<OperationResult<IEnumerable<Invoice>>> GetInvoices(CancellationToken cancellationToken);
        Task<OperationResult<Invoice?>> GetInvoiceById(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<Invoice>> AddInvoice(Invoice invoice, CancellationToken cancellationToken);
        Task<OperationResult<Invoice>> UpdateInvoice(Invoice invoice, CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteInvoice(Guid id, CancellationToken cancellationToken);
    }
}

