using System.Threading;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Domain.Contracts;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class InvoiceService(IInvoiceRepository invoiceRepository) : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

        public async Task<IEnumerable<Invoice>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _invoiceRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Invoice?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Invoice invoice, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.UpdateAsync(invoice, cancellationToken);
        }

        public async Task<bool> AddAsync(Invoice invoice, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.AddAsync(invoice, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
