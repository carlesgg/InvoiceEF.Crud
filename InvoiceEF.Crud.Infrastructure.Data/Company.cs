using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Infrastructure.Data
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
