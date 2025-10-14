using System.Threading;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Domain.Contracts;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class InvoiceLineService(IInvoiceLineRepository invoiceLineRepository) : IInvoiceLineService
    {
        private readonly IInvoiceLineRepository _invoiceLineRepository = invoiceLineRepository;

        public async Task<IEnumerable<InvoiceLine>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _invoiceLineRepository.GetAllAsync(cancellationToken);
        }

        public async Task<InvoiceLine?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _invoiceLineRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(InvoiceLine invoiceLine, CancellationToken cancellationToken)
        {
            return await _invoiceLineRepository.UpdateAsync(invoiceLine, cancellationToken);
        }

        public async Task<bool> AddAsync(InvoiceLine invoiceLine, CancellationToken cancellationToken)
        {
            return await _invoiceLineRepository.AddAsync(invoiceLine, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await _invoiceLineRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
