using InvoiceEF.Crud.Application.Dtos.Requests.InvoiceLine;

namespace InvoiceEF.Crud.Application.Dtos.Requests.Invoice
{
    public class InvoiceRequestsDto
    {
        public Guid ClientId { get; set; }
        public Guid CompanyId { get; set; }
        public DateOnly InvoiceDate { get; set; }
        public decimal Estimate { get; set; }
        public string Signature { get; set; } = string.Empty;
        public List<InvoiceLineRequestsDto> Lines { get; set; } = [];
    }
}
