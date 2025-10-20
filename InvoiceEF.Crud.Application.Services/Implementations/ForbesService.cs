using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Application.Services.Mappers.Implementations;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Infrastructure.Base.Contracts;
using InvoiceEF.Crud.Infrastructure.Base.Implementations;
using InvoiceEF.Crud.Infrastructure.Proxies.Contracts;
using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using InvoiceEF.Crud.Infrastructure.Proxies.Implementations;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class ForbesService(IForbesProxy forbesProxy, IForbesRepository forbesRepository, IUnitOfWork unitOfWork, ForbesDtoToDomainMapper mapper) : IForbesService
    {
        private readonly IForbesProxy _forbesProxy = forbesProxy;
        private readonly ForbesDtoToDomainMapper _mapper = mapper;
        private readonly IForbesRepository _repository = forbesRepository; 
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ForbesPersonDto>> GetBillionairesAsync(CancellationToken cancellationToken)
        {
            return await _forbesProxy.GetListAsync(cancellationToken);
        }

        public async Task SaveBillionairesAsync(IEnumerable<ForbesPersonDto> dtos, CancellationToken cancellationToken)
        {
            var entities = dtos.Select(dto => _mapper.Map(dto));

            foreach (var entity in entities)
            {
                await _repository.AddAsync(entity, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<ForbesPersonDto?> GetBillionaireByRankAsync(int rank, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetBillionaireByRankAsync(rank, cancellationToken);
            if (entity == null) return null;

            // Convertimos la entidad de dominio de nuevo a DTO
            return new ForbesPersonDto
            {
                Uri = entity.Uri,
                Rank = entity.Rank,
                ListUri = entity.ListUri,
                ImageExists = entity.ImageExists,
                FinalWorth = entity.FinalWorth,
                PersonName = entity.PersonName,
                Source = entity.Source,
                Industries = entity.Industries?.Split(',').ToList(),
                CountryOfCitizenship = entity.CountryOfCitizenship,
                Gender = entity.Gender,
                BirthDate = entity.BirthDate.HasValue
                            ? new DateTimeOffset(entity.BirthDate.Value).ToUnixTimeMilliseconds()
                            : null,
                LastName = entity.LastName,
                EstWorthPrev = entity.EstWorthPrev,
                SquareImage = entity.SquareImage
            };
        }

        public async Task DropDatabaseAsync(CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _repository.DeleteAllAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
