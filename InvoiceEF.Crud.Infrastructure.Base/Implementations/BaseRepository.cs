using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using InvoiceEF.Crud.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;


namespace InvoiceEF.Crud.Infrastructure.Base.Implementations
{
    public abstract class BaseRepository<TEntity, TModel> where TModel : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TModel> _dbSet;
        protected readonly IMapper<TEntity, TModel> _mapper;

        protected BaseRepository(AppDbContext context, IMapper<TEntity, TModel> mapper)
        {
            _context = context;
            _dbSet = _context.Set<TModel>();
            _mapper = mapper;
        }

        public virtual async Task<OperationResult<IEnumerable<TEntity>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<TEntity>>();
            try
            {
                var models = await _dbSet.AsNoTracking().ToListAsync(cancellationToken); // Add AsNoTracking for read-only queries
                var domains = models.Select(e => _mapper.MapToDomain(e)).ToList();
                result.AddResult(domains);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }
            return result;
        }

        public virtual async Task<OperationResult<TEntity?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = new OperationResult<TEntity?>();
            try
            {
                var model = await _dbSet.FindAsync([id], cancellationToken);
                if (model == null)
                {
                    result.AddError(404, $"{typeof(TEntity).Name} not found");
                    return result;
                }
                var domainEntity = _mapper.MapToDomain(model);
                result.AddResult(domainEntity);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }
            return result;
        }

        public virtual async Task<OperationResult<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var result = new OperationResult<TEntity>();

            var model = _mapper.MapToDataModel(entity);
            await _dbSet.AddAsync(model, cancellationToken);

            result.AddResult(entity);
            return result;
        }

        public virtual async Task<OperationResult<TEntity>> UpdateAsync(TEntity domainEntity, CancellationToken cancellationToken)
        {
            var result = new OperationResult<TEntity>();

            var id = GetId(domainEntity);
            var existingModel = await _dbSet.FindAsync(id, cancellationToken);
            if (existingModel == null)
            {
                result.AddError(404, $"{typeof(TModel).Name} not found");
                return result;
            }

            UpdateEntity(existingModel, domainEntity);
            _dbSet.Update(existingModel);

            result.AddResult(domainEntity);
            return result;
        }

        public virtual async Task<OperationResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();

            var model = await _dbSet.FindAsync(id, cancellationToken);
            if (model == null)
            {
                result.AddError(404, $"{typeof(TModel).Name} not found");
                result.AddResult(false);
                return result;
            }

            _dbSet.Remove(model);
            result.AddResult(true);
            return result;
        }

        protected abstract void UpdateEntity(TModel model, TEntity domainEntity);
        protected abstract Guid GetId(TEntity domainEntity);
    }
}
