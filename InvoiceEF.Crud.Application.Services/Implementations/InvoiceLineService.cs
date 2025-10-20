using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Base.Contracts;
using InvoiceEF.Crud.Infrastructure.Data;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class InvoiceLineService(IInvoiceLineRepository invoiceLineRepository, IUnitOfWork unitOfWork) : IInvoiceLineService
    {
        private readonly IInvoiceLineRepository _invoiceLineRepository = invoiceLineRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

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
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var addResult = await _invoiceLineRepository.AddInvoiceLine(invoiceLine, cancellationToken);
                if (addResult.HasErrors)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return addResult;
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return addResult;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<OperationResult<InvoiceLineEntity>> UpdateInvoiceLine(InvoiceLineEntity invoiceLine, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var updateResult = await _invoiceLineRepository.AddInvoiceLine(invoiceLine, cancellationToken);
                if (updateResult.HasErrors)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return updateResult;
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return updateResult;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<OperationResult<bool>> DeleteInvoiceLine(Guid id, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var deleteResult = await _invoiceLineRepository.DeleteInvoiceLine(id, cancellationToken);
                if (deleteResult.HasErrors)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return deleteResult;
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return deleteResult;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
