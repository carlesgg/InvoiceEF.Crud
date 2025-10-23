using InvoiceEF.Crud.Application.Mappers.Implementations;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Application.Services.Implementations;
using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Infrastructure.Base.Contracts;
using InvoiceEF.Crud.Infrastructure.Base.Implementations;
using InvoiceEF.Crud.Infrastructure.Proxies.Contracts;
using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using InvoiceEF.Crud.Infrastructure.Proxies.Implementations;
using Microsoft.Extensions.Logging;
using System.ClientModel.Primitives;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class ForbesService(
        ILogger<ForbesService> logger,
        IForbesProxy forbesProxy, 
        IForbesRepository forbesRepository, 
        ICacheService cache, 
        IUnitOfWork unitOfWork 
    ) : IForbesService
    {
        private readonly ILogger<ForbesService> _logger = logger;
        private readonly IForbesProxy _forbesProxy = forbesProxy;
        private readonly IForbesRepository _repository = forbesRepository; 
        private readonly ICacheService _cache = cache;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private const string CacheKey_Billionaires = "billionaires_list";

        public async Task<OperationResult<IEnumerable<ForbesPersonDto>>> GetBillionairesAsync(CancellationToken cancellationToken)
        {
            var operation = new OperationResult<IEnumerable<ForbesPersonDto>>();

            // Try from cache
            var cached = await _cache.GetAsync<string>(CacheKey_Billionaires);
            if (!string.IsNullOrEmpty(cached))
            {
                var list = JsonSerializer.Deserialize<IEnumerable<ForbesPersonDto>>(cached);
                if (list != null)
                {
                    _logger.LogInformation("Returning Forbes list from cache.");
                    return operation.AddResult(list);
                }
            }

            // Fetch from API (proxy)
            var proxyResult = await _forbesProxy.GetListAsync(cancellationToken);

            if (proxyResult.HasErrors || proxyResult.Result == null)
            {
                return operation
                    .AddError(1005, "Failed to retrieve Forbes list from proxy.")
                    .AddErrors(proxyResult.Errors)
                    .AddException(proxyResult.Exception!);
            }

            // Save to cache (store only data)
            var json = JsonSerializer.Serialize(proxyResult.Result);
            await _cache.SetAsync(CacheKey_Billionaires, json, TimeSpan.FromMinutes(30));

            return operation.AddResult(proxyResult.Result);
        }


        public async Task<OperationResult<bool>> SaveBillionairesAsync(
            IEnumerable<ForbesPersonDto> dtos,
            CancellationToken cancellationToken)
        {
            var operation = new OperationResult<bool>();

            try
            {
                var entities = dtos.Select(dto => ForbesApplicationMapper.ToEntity(dto)).ToList();

                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var entity in entities)
                {
                    await _repository.AddAsync(entity, cancellationToken);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                _logger.LogInformation("Insert completed!");

                // Update cache
                var json = JsonSerializer.Serialize(dtos);
                await _cache.SetAsync(CacheKey_Billionaires, json, TimeSpan.FromMinutes(30));

                return operation.AddResult(true);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error saving billionaires");
                return operation.AddException(ex);
            }
        }

        public async Task<OperationResult<ForbesPersonDto?>> GetBillionaireByRankAsync(int rank, CancellationToken cancellationToken)
        {
            var operation = new OperationResult<ForbesPersonDto?>();

            var cachedList = await _cache.GetAsync<string>(CacheKey_Billionaires);
            if (!string.IsNullOrEmpty(cachedList))
            {
                var list = JsonSerializer.Deserialize<IEnumerable<ForbesPersonDto>>(cachedList);
                var dto = list?.FirstOrDefault(f => f.Rank == rank);
                if (dto != null)
                {
                    return operation.AddResult(dto);
                }
            }

            var entity = await _repository.GetBillionaireByRankAsync(rank, cancellationToken);
            if (entity == null)
                return operation.AddError(404, $"No billionaire found with rank {rank}");

            var resultDto = ForbesApplicationMapper.ToResponseDto(entity);

            await _cache.SetAsync($"{CacheKey_Billionaires}_rank_{rank}",
                JsonSerializer.Serialize(resultDto),
                TimeSpan.FromMinutes(30));

            return operation.AddResult(resultDto);
        }

        public async Task<OperationResult<string>> DeleteAllAsync(CancellationToken cancellationToken)
        {
            var operation = new OperationResult<string>();

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _repository.DeleteAllAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                _logger.LogInformation("Delete completed!");

                await _cache.RemoveAsync(CacheKey_Billionaires);

                return operation.AddResult("All billionaires deleted successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error deleting billionaires.");
                return operation.AddError(2002, "Failed to delete billionaires.").AddException(ex);
            }
        }


        public async Task<OperationResult<bool>> DeleteAllAndSeedAsync(
            OperationResult<IEnumerable<ForbesPersonDto>> dtos,
            CancellationToken cancellationToken)
        {
            var operation = new OperationResult<bool>();

            try
            {
                if (dtos.Result == null)
                    return operation.AddError(400, "No data to seed.");

                await _repository.DeleteAllAsync(cancellationToken);

                foreach (var dto in dtos.Result)
                {
                    var entity = ForbesApplicationMapper.ToEntity(dto);
                    await _repository.AddAsync(entity, cancellationToken);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Delete + Insert completed!");

                // Update cache
                await _cache.SetAsync(CacheKey_Billionaires,
                    JsonSerializer.Serialize(dtos.Result),
                    TimeSpan.FromMinutes(30));

                return operation.AddResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting and inserting ForbesPerson entities");
                return operation.AddException(ex);
            }
        }
    }
}
