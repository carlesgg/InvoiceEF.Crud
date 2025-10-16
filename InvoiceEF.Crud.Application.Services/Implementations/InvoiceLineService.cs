using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using System.Threading;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class InvoiceLineService(IInvoiceLineRepository invoiceLineRepository) : IInvoiceLineService
    {
        private readonly IInvoiceLineRepository _invoiceLineRepository = invoiceLineRepository;

        public async Task<OperationResult<IEnumerable<InvoiceLineEntity>>> GetInvoiceLines(CancellationToken cancellationToken)
        {
            return await _invoiceLineRepository.GetInvoiceLines(cancellationToken);
        }

        public async Task<OperationResult<InvoiceLineEntity?>> GetInvoiceLineById(Guid id, CancellationToken cancellationToken)
        {
            return await _invoiceLineRepository.GetInvoiceLineById(id, cancellationToken);
        }

        public async Task<OperationResult<InvoiceLineEntity>> AddInvoiceLine(InvoiceLineEntity invoiceLine, CancellationToken cancellationToken)
        {
            return await _invoiceLineRepository.AddInvoiceLine(invoiceLine, cancellationToken);
        }

        public async Task<OperationResult<InvoiceLineEntity>> UpdateInvoiceLine(InvoiceLineEntity invoiceLine, CancellationToken cancellationToken)
        {
            return await _invoiceLineRepository.UpdateInvoiceLine(invoiceLine, cancellationToken);
        }

        public async Task<OperationResult<bool>> DeleteInvoiceLine(Guid id, CancellationToken cancellationToken)
        {
            return await _invoiceLineRepository.DeleteInvoiceLine(id, cancellationToken);
        }
    }
}
