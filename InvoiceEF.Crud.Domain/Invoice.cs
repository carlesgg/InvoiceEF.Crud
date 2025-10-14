namespace InvoiceEF.Crud.Domain.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int ClientId { get; set; }
        public int CompanyId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Estimate { get; set; }
        public string Signature { get; set; } = string.Empty;
        public List<InvoiceLine> Lines { get; set; } = new();
    }
}
