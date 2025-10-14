using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using System.Threading;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class InvoiceService(IInvoiceRepository invoiceRepository) : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

        public async Task<OperationResult<IEnumerable<Invoice>>> GetInvoices(CancellationToken cancellationToken)
        {
            return await _invoiceRepository.GetInvoices(cancellationToken);
        }

        public async Task<OperationResult<Invoice?>> GetInvoiceById(Guid id, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.GetInvoiceById(id, cancellationToken);
        }

        public async Task<OperationResult<Invoice>> AddInvoice(Invoice invoice, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.AddInvoice(invoice, cancellationToken);
        }

        public async Task<OperationResult<Invoice>> UpdateInvoice(Invoice invoice, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.UpdateInvoice(invoice, cancellationToken);
        }

        public async Task<OperationResult<bool>> DeleteInvoice(Guid id, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.DeleteInvoice(id, cancellationToken);
        }
    }
}
