using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Application.Dtos.Responses.InvoiceLine
{
    public class InvoiceLineResponse
    {
        public Guid LineId { get; set; }
        public string Concept { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal LineTotal { get; set; }
    }
}
