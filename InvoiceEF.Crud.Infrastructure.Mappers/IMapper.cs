namespace InvoiceEF.Crud.Infrastructure.Mappers
{
    public interface IMapper<TDomain, TModel>
    {
        TDomain MapToDomain(TModel model);
        TModel MapToDataModel(TDomain domainEntity);
    }
}
