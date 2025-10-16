namespace InvoiceEF.Crud.Application.Mappers
{
    public interface IRequestMapper<TEntity, TRequestDto>
    {
        TEntity MapToDomain(TRequestDto dto);
        TRequestDto MapToDataModel(TEntity domainEntity);
    }
}
