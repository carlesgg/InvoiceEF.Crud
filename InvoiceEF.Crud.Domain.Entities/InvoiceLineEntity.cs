namespace InvoiceEF.Crud.Domain.Entities
{
    public class InvoiceLineEntity(Guid lineId, Guid invoiceId, string concept, int quantity, decimal price)
    {
        public Guid LineId { get; set; } = lineId;
        public Guid InvoiceId { get; set; } = invoiceId;
        public string Concept { get; set; } = concept;
        public int Quantity { get; set; } = quantity;
        public decimal Price { get; set; } = price;
        public decimal LineTotal => Quantity * Price;

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
            Quantity = quantity;
        }
    }
}
