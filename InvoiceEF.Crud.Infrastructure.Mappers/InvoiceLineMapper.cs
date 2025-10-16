using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Data;

namespace InvoiceEF.Crud.Infrastructure.Mappers
{
    public class InvoiceLineMapper : IMapper<InvoiceLineEntity, InvoiceLine>
    {
        public InvoiceLineEntity MapToDomain(InvoiceLine dataModel)
        {
            return new InvoiceLineEntity(dataModel.LineId, dataModel.InvoiceId, dataModel.Concept, dataModel.Quantity, dataModel.Price);
        }

        public InvoiceLine MapToDataModel(InvoiceLineEntity domainEntity)
        {
            return new InvoiceLine
            {
                LineId = domainEntity.LineId,
                InvoiceId = domainEntity.InvoiceId,
                Concept = domainEntity.Concept,
                Quantity = domainEntity.Quantity,
                Price = domainEntity.Price,

            };
        }
    }
}
