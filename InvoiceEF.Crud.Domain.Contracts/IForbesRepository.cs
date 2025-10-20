using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Domain.Contracts
{
    public interface IForbesRepository : IBaseRepository<ForbesPersonEntity>
    {
        // Métodos específicos del repositorio, si los necesitas
        Task<IEnumerable<ForbesPersonEntity>> GetTopBillionairesAsync(int top, CancellationToken cancellationToken);
        Task<ForbesPersonEntity?> GetBillionaireByRankAsync(int rank, CancellationToken cancellationToken);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken);
    }
}