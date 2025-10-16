using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Infrastructure.Data
{
    public class InvoiceLine
    {
        public Guid LineId { get; set; }
        public Guid InvoiceId { get; set; }
        public string Concept { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
