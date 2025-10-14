using System.Threading;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Domain.Contracts;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class ClientService(IClientRepository clientRepository) : IClientService
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public async Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _clientRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _clientRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Client client, CancellationToken cancellationToken)
        {
            return await _clientRepository.UpdateAsync(client, cancellationToken);
        }

        public async Task<bool> AddAsync(Client client, CancellationToken cancellationToken)
        {
            return await _clientRepository.AddAsync(client, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await _clientRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
