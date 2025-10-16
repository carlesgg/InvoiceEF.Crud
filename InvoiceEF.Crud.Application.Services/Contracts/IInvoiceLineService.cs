using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Application.Services.Contracts
{
    public interface IInvoiceLineService
    {
        Task<OperationResult<IEnumerable<InvoiceLineEntity>>> GetInvoiceLines(CancellationToken cancellationToken);
        Task<OperationResult<InvoiceLineEntity?>> GetInvoiceLineById(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<InvoiceLineEntity>> AddInvoiceLine(InvoiceLineEntity invoiceLine, CancellationToken cancellationToken);
        Task<OperationResult<InvoiceLineEntity>> UpdateInvoiceLine(InvoiceLineEntity invoiceLine, CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteInvoiceLine(Guid id, CancellationToken cancellationToken);
    }
}
