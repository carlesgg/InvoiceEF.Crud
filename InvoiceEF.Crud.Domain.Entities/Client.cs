namespace InvoiceEF.Crud.Domain.Entities
{
    public class Client
    {
        public Guid ClientId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

    }
}
