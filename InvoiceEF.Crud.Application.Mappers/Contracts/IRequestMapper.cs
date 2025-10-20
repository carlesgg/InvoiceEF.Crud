namespace InvoiceEF.Crud.Application.Mappers.Contracts
{
    public interface IRequestMapper<TEntity, TCreateDto, TUpdateDto>
    {
        TEntity ToEntity(TCreateDto createDto);
        TEntity ToEntity(TUpdateDto updateDto);
    }
}
