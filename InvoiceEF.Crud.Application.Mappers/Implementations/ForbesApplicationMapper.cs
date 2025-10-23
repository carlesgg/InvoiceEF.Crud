using InvoiceEF.Crud.Application.Mappers.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using System;

namespace InvoiceEF.Crud.Application.Mappers.Implementations
{
    public static class ForbesApplicationMapper
    {
        public static ForbesPersonEntity ToEntity(ForbesPersonDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

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

        public static ForbesPersonDto ToResponseDto(this ForbesPersonEntity entity)
        {
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

        public static IEnumerable<ForbesPersonDto> ToResponseDtos(this IEnumerable<ForbesPersonEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
