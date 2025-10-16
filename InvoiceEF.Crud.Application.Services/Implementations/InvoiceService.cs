using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Base.Contracts;
using System.Threading;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class InvoiceService(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork) : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<OperationResult<IEnumerable<InvoiceEntity>>> GetInvoices(CancellationToken cancellationToken)
        {
            return await _invoiceRepository.GetInvoices(cancellationToken);
        }

        public async Task<OperationResult<InvoiceEntity?>> GetInvoiceById(Guid id, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.GetInvoiceById(id, cancellationToken);
        }

        public async Task<OperationResult<InvoiceEntity>> AddInvoice(InvoiceEntity invoice, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var addResult = await _invoiceRepository.AddInvoice(invoice, cancellationToken);
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

        public async Task<OperationResult<InvoiceEntity>> UpdateInvoice(InvoiceEntity invoice, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var updateResult = await _invoiceRepository.UpdateInvoice(invoice, cancellationToken);
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

        public async Task<OperationResult<bool>> DeleteInvoice(Guid id, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var deleteResult = await _invoiceRepository.DeleteInvoice(id, cancellationToken);
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
