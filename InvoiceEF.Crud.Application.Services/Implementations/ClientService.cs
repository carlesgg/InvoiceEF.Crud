using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using System.Threading;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class ClientService(IClientRepository clientRepository) : IClientService
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public async Task<OperationResult<IEnumerable<Client>>> GetClients(CancellationToken cancellationToken)
        {
            return await _clientRepository.GetClients(cancellationToken);
        }

        public async Task<OperationResult<Client?>> GetClientById(Guid id, CancellationToken cancellationToken)
        {
            return await _clientRepository.GetClientById(id, cancellationToken);
        }

        public async Task<OperationResult<Client>> AddClient(Client client, CancellationToken cancellationToken)
        {
            return await _clientRepository.AddClient(client, cancellationToken);
        }

        public async Task<OperationResult<Client>> UpdateClient(Client client, CancellationToken cancellationToken)
        {
            return await _clientRepository.UpdateClient(client, cancellationToken);
        }

        public async Task<OperationResult<bool>> DeleteClient(Guid id, CancellationToken cancellationToken)
        {
            return await _clientRepository.DeleteClient(id, cancellationToken);
        }
    }
}
