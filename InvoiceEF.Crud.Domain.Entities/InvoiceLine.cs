namespace InvoiceEF.Crud.Domain.Entities
{
    public class InvoiceLine
    {
        public Guid LineId { get; set; }
        public Guid InvoiceId { get; set; }
        public string Concept { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal LineTotal { get; set; }

    }
}
