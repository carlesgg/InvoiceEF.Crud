using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Application.Services.Contracts
{
    public interface IInvoiceLineService
    {
        Task<OperationResult<IEnumerable<InvoiceLine>>> GetInvoiceLines(CancellationToken cancellationToken);
        Task<OperationResult<InvoiceLine?>> GetInvoiceLineById(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<InvoiceLine>> AddInvoiceLine(InvoiceLine invoiceLine, CancellationToken cancellationToken);
        Task<OperationResult<InvoiceLine>> UpdateInvoiceLine(InvoiceLine invoiceLine, CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteInvoiceLine(Guid id, CancellationToken cancellationToken);
    }
}
