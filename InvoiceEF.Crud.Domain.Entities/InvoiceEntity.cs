using InvoiceEF.Crud.Infrastructure.Data;

namespace InvoiceEF.Crud.Domain.Entities
{
    public class InvoiceEntity(Guid invoiceId, Guid clientId, Guid companyId, DateTime invoiceDate, decimal estimate, string signature)
    {
        public Guid InvoiceId { get; private set; } = invoiceId;
        public Guid ClientId { get; private set; } = clientId;
        public Guid CompanyId { get; private set; } = companyId;
        public DateTime InvoiceDate { get; private set; } = invoiceDate;
        public decimal Estimate { get; private set; } = estimate;
        public string Signature { get; private set; } = signature;
        public List<InvoiceLineEntity> Lines { get; private set; } = [];

        public decimal Total => Lines.Sum(l => l.LineTotal);
    }
}
