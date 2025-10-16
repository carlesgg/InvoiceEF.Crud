using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Application.Dtos.Requests.InvoiceLine
{
    public class InvoiceLineRequestsDto
    {
        public string Concept { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
