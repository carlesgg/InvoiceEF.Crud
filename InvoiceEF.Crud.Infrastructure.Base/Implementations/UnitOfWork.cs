using InvoiceEF.Crud.Infrastructure.Base.Contracts;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using Microsoft.EntityFrameworkCore.Storage;

namespace InvoiceEF.Crud.Infrastructure.Base.Implementations
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;
        private IDbContextTransaction? _transaction;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _transaction ??= await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(cancellationToken);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            _transaction?.Dispose();
        }
    }
}
