using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class InvoiceLineRepository(AppDbContext context) : IInvoiceLineRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<OperationResult<IEnumerable<InvoiceLine>>> GetInvoiceLines(CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<InvoiceLine>>();
            try
            {
                var invoiceLines = await _context.InvoiceLines.ToListAsync(cancellationToken);
                result.AddResult(invoiceLines);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }
            return result;
        }

        public async Task<OperationResult<InvoiceLine?>> GetInvoiceLineById(Guid id, CancellationToken cancellationToken)
        {
            var result = new OperationResult<InvoiceLine?>();
            try
            {
                var invoiceLine = await _context.InvoiceLines.FindAsync([id], cancellationToken);
                result.AddResult(invoiceLine);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }
            return result;
        }

        public async Task<OperationResult<InvoiceLine>> AddInvoiceLine(InvoiceLine invoiceLine, CancellationToken cancellationToken)
        {
            var result = new OperationResult<InvoiceLine>();
            try
            {
                await _context.InvoiceLines.AddAsync(invoiceLine, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                result.AddResult(invoiceLine);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }
            return result;
        }

        
        public async Task<OperationResult<InvoiceLine>> UpdateInvoiceLine(InvoiceLine invoiceLine, CancellationToken cancellationToken)
        {
            var result = new OperationResult<InvoiceLine>();
            try
            {
                var existing = await _context.InvoiceLines.FindAsync([invoiceLine.LineId], cancellationToken);
                if (existing == null)
                {
                    result.AddError(404, "InvoiceLine not found");
                    return result;
                }

                existing.InvoiceId = invoiceLine.InvoiceId;
                existing.Concept = invoiceLine.Concept;
                existing.Quantity = invoiceLine.Quantity;
                existing.Price = invoiceLine.Price;
                existing.LineTotal = invoiceLine.LineTotal;

                await _context.SaveChangesAsync(cancellationToken);
                result.AddResult(existing);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }
            return result;
        }

        public async Task<OperationResult<bool>> DeleteInvoiceLine(Guid id, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            try
            {
                var existing = await _context.InvoiceLines.FindAsync([id], cancellationToken);
                if (existing == null)
                {
                    result.AddError(404, "InvoiceLine not found");
                    result.AddResult(false);
                    return result;
                }

                _context.InvoiceLines.Remove(existing);
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
