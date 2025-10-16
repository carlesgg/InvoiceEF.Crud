using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Application.Services.Contracts
{
    public interface IInvoiceService
    {
        Task<OperationResult<IEnumerable<InvoiceEntity>>> GetInvoices(CancellationToken cancellationToken);
        Task<OperationResult<InvoiceEntity?>> GetInvoiceById(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<InvoiceEntity>> AddInvoice(InvoiceEntity invoice, CancellationToken cancellationToken);
        Task<OperationResult<InvoiceEntity>> UpdateInvoice(InvoiceEntity invoice, CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteInvoice(Guid id, CancellationToken cancellationToken);
    }
}
