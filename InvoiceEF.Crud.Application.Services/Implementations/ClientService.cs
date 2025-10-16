using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class ClientService(IClientRepository clientRepository) : IClientService
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public async Task<OperationResult<IEnumerable<ClientEntity>>> GetClients(CancellationToken cancellationToken)
        {
            return await _clientRepository.GetClients(cancellationToken);
        }

        public async Task<OperationResult<ClientEntity?>> GetClientById(Guid id, CancellationToken cancellationToken)
        {
            return await _clientRepository.GetClientById(id, cancellationToken);
        }

        public async Task<OperationResult<ClientEntity>> AddClient(ClientEntity client, CancellationToken cancellationToken)
        {
            return await _clientRepository.AddClient(client, cancellationToken);
        }

        public async Task<OperationResult<ClientEntity>> UpdateClient(ClientEntity client, CancellationToken cancellationToken)
        {
            return await _clientRepository.UpdateClient(client, cancellationToken);
        }

        public async Task<OperationResult<bool>> DeleteClient(Guid id, CancellationToken cancellationToken)
        {
            return await _clientRepository.DeleteClient(id, cancellationToken);
        }
    }
}
