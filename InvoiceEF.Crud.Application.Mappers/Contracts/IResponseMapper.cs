namespace InvoiceEF.Crud.Application.Mappers.Contracts
{
    public interface IResponseMapper<TEntity, TResponseDto>
    {
        TResponseDto ToResponseDto(TEntity entity);
        IEnumerable<TResponseDto> ToResponseDtos(IEnumerable<TEntity> entities);
    }
}
