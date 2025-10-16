using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Base.Implementations;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using InvoiceEF.Crud.Infrastructure.Data;
using InvoiceEF.Crud.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class CompanyRepository(AppDbContext context, IMapper<CompanyEntity, Company> mapper) : BaseRepository<CompanyEntity, Company>(context, mapper), ICompanyRepository
    {

        public Task<OperationResult<IEnumerable<CompanyEntity>>> GetCompanies(CancellationToken cancellationToken)
            => GetAllAsync(cancellationToken);

        public Task<OperationResult<CompanyEntity?>> GetCompanyById(Guid id, CancellationToken cancellationToken)
            => GetByIdAsync(id, cancellationToken);

        public Task<OperationResult<CompanyEntity>> AddCompany(CompanyEntity company, CancellationToken cancellationToken)
            => AddAsync(company, cancellationToken);

        public Task<OperationResult<CompanyEntity>> UpdateCompany(CompanyEntity company, CancellationToken cancellationToken)
            => UpdateAsync(company, cancellationToken);

        public Task<OperationResult<bool>> DeleteCompany(Guid id, CancellationToken cancellationToken)
            => DeleteAsync(id, cancellationToken);

        protected override void UpdateEntity(Company model, CompanyEntity domainEntity)
        {
            model.Name = domainEntity.Name;
            model.Direction = domainEntity.Direction;
            model.Email = domainEntity.Email;
            model.Phone = domainEntity.Phone;
        }

        protected override Guid GetId(CompanyEntity domainEntity)
        {
            return domainEntity.CompanyId;
        }
    }
}
