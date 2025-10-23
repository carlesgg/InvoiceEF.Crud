using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Application.Services.Contracts
{
    public interface IForbesService
    {
        Task<OperationResult<IEnumerable<ForbesPersonDto>>> GetBillionairesAsync(CancellationToken cancellationToken);
        Task<OperationResult<bool>> SaveBillionairesAsync(IEnumerable<ForbesPersonDto> dtos, CancellationToken cancellationToken);
        Task<OperationResult<ForbesPersonDto?>> GetBillionaireByRankAsync(int rank, CancellationToken cancellationToken);
        Task<OperationResult<string>> DeleteAllAsync(CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteAllAndSeedAsync(OperationResult<IEnumerable<ForbesPersonDto>> dtos, CancellationToken cancellationToken);
    }
}
