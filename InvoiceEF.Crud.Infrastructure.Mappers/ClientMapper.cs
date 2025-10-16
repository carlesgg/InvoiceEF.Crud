using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Data;

namespace InvoiceEF.Crud.Infrastructure.Mappers
{
    public class ClientMapper : IMapper<ClientEntity, Client>
    {
        public ClientEntity MapToDomain(Client dataModel)
        {
            return new ClientEntity(dataModel.ClientId, dataModel.Name, dataModel.Direction, dataModel.Email, dataModel.Phone);
        }

        public Client MapToDataModel(ClientEntity domainEntity)
        {
            return new Client
            {
                ClientId = domainEntity.ClientId,
                Name = domainEntity.Name,
                Direction = domainEntity.Direction,
                Email = domainEntity.Email,
                Phone = domainEntity.Phone
            };
        }
    }
}
