using InvoiceEF.Crud.Infrastructure.Data;

namespace InvoiceEF.Crud.Domain.Entities
{
    public class CompanyEntity(Guid companyId, string name, string direction, string email, string phone)
    {
        public Guid CompanyId { get; set; } = companyId;
        public string Name { get; set; } = name;
        public string Direction { get; set; } = direction;
        public string Email { get; set; } = email;
        public string Phone { get; set; } = phone;
    }
}
