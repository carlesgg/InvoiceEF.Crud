using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Infrastructure.Base.Contracts;

namespace InvoiceEF.Crud.Application.Bases
{
    public abstract class BaseService(IUnitOfWork unitOfWork)
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;

        protected async Task<OperationResult<T>> ExecuteInTransactionAsync<T>(
            Func<Task<OperationResult<T>>> action,
            CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await action();

                if (result.HasErrors)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return result;
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return result;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw; // middleware lo captura
            }
        }
    }
}
