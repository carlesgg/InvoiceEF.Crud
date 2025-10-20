using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Application.Services.Contracts
{
    public interface IForbesService
    {
        Task<IEnumerable<ForbesPersonDto>> GetBillionairesAsync(CancellationToken cancellationToken);
        Task SaveBillionairesAsync(IEnumerable<ForbesPersonDto> dtos, CancellationToken cancellationToken);
        Task<ForbesPersonDto?> GetBillionaireByRankAsync(int rank, CancellationToken cancellationToken);
        Task DropDatabaseAsync(CancellationToken cancellationToken);
    }
}
