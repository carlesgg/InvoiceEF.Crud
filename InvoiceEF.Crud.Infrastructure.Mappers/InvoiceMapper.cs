using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Data;

namespace InvoiceEF.Crud.Infrastructure.Mappers
{
    public class InvoiceMapper : IMapper<InvoiceEntity, Invoice>
    {
        public InvoiceEntity MapToDomain(Invoice dataModel)
        {
            List<InvoiceLineEntity> domainLines = [.. dataModel.Lines
                .Select(static line => new InvoiceLineEntity(
                    line.LineId,
                    line.InvoiceId,
                    line.Concept,
                    line.Quantity,
                    line.Price))];

            return new InvoiceEntity(dataModel.InvoiceId, dataModel.ClientId, dataModel.CompanyId, dataModel.InvoiceDate, dataModel.Estimate, dataModel.Signature);
        }

        public Invoice MapToDataModel(InvoiceEntity domainEntity)
        {
            return new Invoice
            {
                InvoiceId = domainEntity.InvoiceId,
                ClientId = domainEntity.ClientId,
                CompanyId = domainEntity.CompanyId,
                InvoiceDate = domainEntity.InvoiceDate,
                Estimate = domainEntity.Estimate,
                Signature = domainEntity.Signature,
                Lines = [.. domainEntity.Lines.Select(static line => new InvoiceLine
                {
                    LineId = line.LineId,
                    InvoiceId = line.InvoiceId,
                    Concept = line.Concept,
                    Quantity = line.Quantity,
                    Price = line.Price,
                })]

            };
        }
    }
}
