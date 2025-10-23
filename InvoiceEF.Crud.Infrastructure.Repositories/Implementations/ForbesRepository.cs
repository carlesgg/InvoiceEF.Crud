using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Base.Implementations;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using InvoiceEF.Crud.Infrastructure.Data;
using InvoiceEF.Crud.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class ForbesRepository(AppDbContext context, IMapper<ForbesPersonEntity, ForbesPerson> mapper) : BaseRepository<ForbesPersonEntity, ForbesPerson>(context, mapper), IForbesRepository
    {
        public async Task<IEnumerable<ForbesPersonEntity>> GetTopBillionairesAsync(int top, CancellationToken cancellationToken)
        {
            var models = await _dbSet
                .OrderByDescending(f => f.FinalWorth)
                .Take(top)
                .ToListAsync(cancellationToken);
            
            return models.Select(m => _mapper.MapToDomain(m));
        }

        public async Task<ForbesPersonEntity?> GetBillionaireByRankAsync(int rank, CancellationToken cancellationToken)
        {
            var model = await _dbSet.FirstOrDefaultAsync(f => f.Rank == rank, cancellationToken);
            return model != null ? _mapper.MapToDomain(model) : null;
        }

        protected override Guid GetId(ForbesPersonEntity domainEntity)
        {
            // Retorna la propiedad que actúa como Id
            return domainEntity.Id;
        }

        protected override void UpdateEntity(ForbesPerson model, ForbesPersonEntity domainEntity)
        {
            // Copia los valores del DataModel a la entidad de dominio
            domainEntity.Uri = model.Uri;
            domainEntity.Rank = model.Rank;
            domainEntity.ListUri = model.ListUri;
            domainEntity.ImageExists = model.ImageExists;
            domainEntity.FinalWorth = model.FinalWorth;
            domainEntity.PersonName = model.PersonName;
            domainEntity.Source = model.Source;
            domainEntity.Industries = model.Industries != null ? string.Join(",", model.Industries) : null;
            domainEntity.CountryOfCitizenship = model.CountryOfCitizenship;
            domainEntity.Gender = model.Gender;
            domainEntity.BirthDate = model.BirthDate;
            domainEntity.LastName = model.LastName;
            domainEntity.EstWorthPrev = model.EstWorthPrev;
            domainEntity.SquareImage = model.SquareImage;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var entities = await _dbSet.ToListAsync(cancellationToken);
                if (entities.Count == 0)
                {   
                    return false;
                }

                _dbSet.RemoveRange(entities);
                if (entities.Count == 0)
                {

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting all ForbesPerson entities", ex);
            }
            return false;
        }
    }
}
