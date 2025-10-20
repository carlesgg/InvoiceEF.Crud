using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Data;
using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Infrastructure.Mappers
{
    public class ForbesMapper : IMapper<ForbesPersonEntity, ForbesPerson>
    {
        public ForbesPersonEntity MapToDomain(ForbesPerson model)
        {
            return new ForbesPersonEntity
            {
                Id = model.Id,
                Uri = model.Uri,
                Rank = model.Rank,
                ListUri = model.ListUri,
                ImageExists = model.ImageExists,
                FinalWorth = model.FinalWorth,
                PersonName = model.PersonName,
                Source = model.Source,
                Industries = model.Industries,
                CountryOfCitizenship = model.CountryOfCitizenship,
                Gender = model.Gender,
                BirthDate = model.BirthDate,
                LastName = model.LastName,
                EstWorthPrev = model.EstWorthPrev,
                SquareImage = model.SquareImage
            };
        }

        public ForbesPerson MapToDataModel(ForbesPersonEntity entity)
        {
            return new ForbesPerson
            {
                Id = entity.Id,
                Uri = entity.Uri,
                Rank = entity.Rank,
                ListUri = entity.ListUri,
                ImageExists = entity.ImageExists,
                FinalWorth = entity.FinalWorth,
                PersonName = entity.PersonName,
                Source = entity.Source,
                Industries = entity.Industries,
                CountryOfCitizenship = entity.CountryOfCitizenship,
                Gender = entity.Gender,
                BirthDate = entity.BirthDate,
                LastName = entity.LastName,
                EstWorthPrev = entity.EstWorthPrev,
                SquareImage = entity.SquareImage
            };
        }
    }

}
