using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Application.Services.Contracts
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken);
        Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(Client client, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Client client, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
