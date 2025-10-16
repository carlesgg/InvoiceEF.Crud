namespace InvoiceEF.Crud.Infrastructure.Mappers
{
    public interface IMapper<TDomain, TModel>
    {
        TDomain MapToDomain(TModel dataModel);
        TModel MapToDataModel(TDomain domainEntity);
    }
}
