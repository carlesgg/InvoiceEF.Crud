namespace InvoiceEF.Crud.Domain.Entities
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }
        public Guid ClientId { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Estimate { get; set; }
        public string Signature { get; set; } = string.Empty;
        public List<InvoiceLine> Lines { get; set; } = [];
    }
}
