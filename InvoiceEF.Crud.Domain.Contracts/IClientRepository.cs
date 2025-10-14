using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Domain.Contracts
{
    public interface IClientRepository
    {
        Task<OperationResult<IEnumerable<Client>>> GetClients(CancellationToken cancellationToken);
        Task<OperationResult<Client?>> GetClientById(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<Client>> AddClient(Client client, CancellationToken cancellationToken);
        Task<OperationResult<Client>> UpdateClient(Client client, CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteClient(Guid id, CancellationToken cancellationToken);
    }
}
