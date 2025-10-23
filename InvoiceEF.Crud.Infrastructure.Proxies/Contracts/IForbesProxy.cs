using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Infrastructure.Proxies.Contracts
{
    public interface IForbesProxy
    {
        Task<OperationResult<IEnumerable<ForbesPersonDto>>> GetListAsync(CancellationToken cancellationToken);
    }
}
