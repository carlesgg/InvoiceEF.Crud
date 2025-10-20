using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Infrastructure.Proxies.Dtos
{
    public record ForbesApiResponse
    {
        public string? Status { get; set; }
        public List<ForbesPersonDto>? Data { get; set; }
    }
}
