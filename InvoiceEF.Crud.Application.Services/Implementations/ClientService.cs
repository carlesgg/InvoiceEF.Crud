using InvoiceEF.Crud.Application.Bases;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Base.Contracts;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork) : BaseService(unitOfWork), IClientService
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        // Métodos de solo lectura, no necesitan transacción
        public Task<OperationResult<IEnumerable<ClientEntity>>> GetClients(CancellationToken cancellationToken)
        {
            return _clientRepository.GetClients(cancellationToken);
        }

        public Task<OperationResult<ClientEntity?>> GetClientById(Guid id, CancellationToken cancellationToken)
        {
            return _clientRepository.GetClientById(id, cancellationToken);
        }

        // Métodos que modifican datos, usan ExecuteInTransactionAsync
        public Task<OperationResult<ClientEntity>> AddClient(ClientEntity client, CancellationToken cancellationToken)
        {
            return ExecuteInTransactionAsync(
                () => _clientRepository.AddClient(client, cancellationToken),
                cancellationToken);
        }

        public Task<OperationResult<ClientEntity>> UpdateClient(ClientEntity client, CancellationToken cancellationToken)
        {
            return ExecuteInTransactionAsync(
                () => _clientRepository.UpdateClient(client, cancellationToken),
                cancellationToken);
        }

        public Task<OperationResult<bool>> DeleteClient(Guid id, CancellationToken cancellationToken)
        {
            return ExecuteInTransactionAsync(
                () => _clientRepository.DeleteClient(id, cancellationToken),
                cancellationToken);
        }
    }
}
