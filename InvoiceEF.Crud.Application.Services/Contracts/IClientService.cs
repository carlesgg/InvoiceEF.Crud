using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Application.Services.Contracts
{
    public interface IClientService
    {
        Task<OperationResult<IEnumerable<ClientEntity>>> GetClients(CancellationToken cancellationToken);
        Task<OperationResult<ClientEntity?>> GetClientById(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<ClientEntity>> AddClient(ClientEntity client, CancellationToken cancellationToken);
        Task<OperationResult<ClientEntity>> UpdateClient(ClientEntity client, CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteClient(Guid id, CancellationToken cancellationToken);
    }
}

