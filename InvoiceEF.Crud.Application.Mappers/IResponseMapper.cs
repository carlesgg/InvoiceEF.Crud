namespace InvoiceEF.Crud.Application.Mappers
{
    public interface IMapper<TEntity, TResponseDto>
    {
        TEntity MapToDomain(TResponseDto dto);
        TResponseDto MapToDataModel(TEntity domainEntity);
    }
}
