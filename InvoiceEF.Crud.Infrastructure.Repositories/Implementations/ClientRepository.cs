using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Base.Implementations;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using InvoiceEF.Crud.Infrastructure.Data;
using InvoiceEF.Crud.Infrastructure.Mappers;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class ClientRepository(AppDbContext context, IMapper<ClientEntity, Client> mapper) : BaseRepository<ClientEntity, Client>(context, mapper), IClientRepository
    {
       
        public Task<OperationResult<IEnumerable<ClientEntity>>> GetClients(CancellationToken cancellationToken = default)
            => GetAllAsync(cancellationToken);

        public Task<OperationResult<ClientEntity?>> GetClientById(Guid id, CancellationToken cancellationToken = default)
            => GetByIdAsync(id, cancellationToken);

        public Task<OperationResult<ClientEntity>> AddClient(ClientEntity clientEntity, CancellationToken cancellationToken = default)
            => AddAsync(clientEntity, cancellationToken);

        public Task<OperationResult<ClientEntity>> UpdateClient(ClientEntity clientEntity, CancellationToken cancellationToken = default)
            => UpdateAsync(clientEntity, cancellationToken);

        public Task<OperationResult<bool>> DeleteClient(Guid id, CancellationToken cancellationToken = default)
            => DeleteAsync(id, cancellationToken);

        protected override void UpdateEntity(Client model, ClientEntity domainEntity)
        {
            model.Name = domainEntity.Name;
            model.Direction = domainEntity.Direction;
            model.Email = domainEntity.Email;
            model.Phone = domainEntity.Phone;
        }

        protected override Guid GetId(ClientEntity domainEntity)
        {
            return domainEntity.ClientId;
        }
    }
}
