using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using Microsoft.EntityFrameworkCore;
using System.ClientModel.Primitives;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class InvoiceRepository(AppDbContext context) : IInvoiceRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<OperationResult<IEnumerable<Invoice>>> GetInvoices(CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<Invoice>>();
            try
            {
                var invoices = await _context.Invoices.ToListAsync(cancellationToken);
                result.AddResult(invoices);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }
            return result;
        }

        // Obtener una factura por ID
        public async Task<OperationResult<Invoice?>> GetInvoiceById(Guid id, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Invoice?>();
            try
            {
                var invoice = await _context.Invoices.FindAsync([id], cancellationToken);
                if (invoice == null)
                {
                    result.AddError(404, "Invoice not found");
                    return result;
                }

                result.AddResult(invoice);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }
            return result;
        }

        // Agregar una nueva factura
        public async Task<OperationResult<Invoice>> AddInvoice(Invoice invoice, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Invoice>();
            try
            {
                await _context.Invoices.AddAsync(invoice, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                result.AddResult(invoice);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }
            return result;
        }

        // Actualizar una factura existente
        public async Task<OperationResult<Invoice>> UpdateInvoice(Invoice invoice, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Invoice>();
            try
            {
                var existing = await _context.Invoices.FindAsync([invoice.InvoiceId], cancellationToken);
                if (existing == null)
                {
                    result.AddError(404, "Invoice not found");
                    return result;
                }

                existing.ClientId = invoice.ClientId;
                existing.CompanyId = invoice.CompanyId;
                existing.InvoiceDate = invoice.InvoiceDate;
                existing.Estimate = invoice.Estimate;
                existing.Signature = invoice.Signature;

                await _context.SaveChangesAsync(cancellationToken);
                result.AddResult(existing);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }
            return result;
        }

        // Eliminar una factura
        public async Task<OperationResult<bool>> DeleteInvoice(Guid id, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            try
            {
                var existing = await _context.Invoices.FindAsync([id], cancellationToken);
                if (existing == null)
                {
                    result.AddError(404, "Invoice not found");
                    result.AddResult(false);
                    return result;
                }

                _context.Invoices.Remove(existing);
                await _context.SaveChangesAsync(cancellationToken);
                result.AddResult(true);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }
            return result;
        }
    }
}
