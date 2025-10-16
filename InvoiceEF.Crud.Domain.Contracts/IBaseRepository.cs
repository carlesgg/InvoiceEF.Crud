using InvoiceEF.Crud.CrossCutting;

namespace InvoiceEF.Crud.Domain.Contracts
{
    public interface IBaseRepository<TEntity>
    {
        Task<OperationResult<IEnumerable<TEntity>>> GetAllAsync(CancellationToken cancellationToken);
        Task<OperationResult<TEntity?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
