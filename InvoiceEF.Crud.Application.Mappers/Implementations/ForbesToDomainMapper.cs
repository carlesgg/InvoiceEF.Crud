using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using System;

namespace InvoiceEF.Crud.Application.Services.Mappers.Implementations
{
    public class ForbesDtoToDomainMapper
    {
        public ForbesPersonEntity Map(ForbesPersonDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new ForbesPersonEntity
            {
                Uri = dto.Uri ?? string.Empty,
                Rank = dto.Rank ?? 0,
                ListUri = dto.ListUri,
                ImageExists = dto.ImageExists ?? false,
                FinalWorth = dto.FinalWorth ?? 0,
                PersonName = dto.PersonName ?? string.Empty,
                Source = dto.Source,
                Industries = dto.Industries != null ? string.Join(",", dto.Industries) : null,
                CountryOfCitizenship = dto.CountryOfCitizenship,
                Gender = dto.Gender,
                BirthDate = dto.BirthDate.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(dto.BirthDate.Value).DateTime : null,
                LastName = dto.LastName,
                EstWorthPrev = dto.EstWorthPrev,
                SquareImage = dto.SquareImage
            };
        }
    }
}
