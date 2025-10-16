namespace InvoiceEF.Crud.Domain.Entities
{
    public class ClientEntity
    {
        public Guid ClientId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public ClientEntity(Guid clientId, string name, string direction, string email, string phone)
        {
            ClientId = clientId;
            Name = name;
            Direction = direction;
            Email = email;
            Phone = phone;
        }
    }
}
