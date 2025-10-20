using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Application.Dtos.Requests.InvoiceLine
{
    public record InvoiceLineCreateDto(
        Guid lineId,
        Guid invoiceId,
        string concept,
        int quantity,
        decimal price
    );
}
