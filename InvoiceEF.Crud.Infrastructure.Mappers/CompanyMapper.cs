using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Data;

namespace InvoiceEF.Crud.Infrastructure.Mappers
{
    public class CompanyMapper : IMapper<CompanyEntity, Company>
    {
        public CompanyEntity MapToDomain(Company dataModel)
        {
            return new CompanyEntity(dataModel.CompanyId, dataModel.Name, dataModel.Direction, dataModel.Email, dataModel.Phone);
        }

        public Company MapToDataModel(CompanyEntity domainEntity)
        {
            return new Company
            {
                CompanyId = domainEntity.CompanyId,
                Name = domainEntity.Name,
                Direction = domainEntity.Direction,
                Email = domainEntity.Email,
                Phone = domainEntity.Phone
            };
        }
    }
}
