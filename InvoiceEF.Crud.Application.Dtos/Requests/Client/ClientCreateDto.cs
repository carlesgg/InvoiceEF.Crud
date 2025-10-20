using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Application.Dtos.Requests.Client
{
    public record ClientCreateDto(
        string Name,
        string Direction,
        string Email,
        string Phone
    );
}
