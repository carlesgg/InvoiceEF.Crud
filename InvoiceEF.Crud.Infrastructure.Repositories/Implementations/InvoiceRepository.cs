using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Base.Implementations;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using InvoiceEF.Crud.Infrastructure.Data;
using InvoiceEF.Crud.Infrastructure.Mappers;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class InvoiceRepository(AppDbContext context, IMapper<InvoiceEntity, Invoice> mapper) : BaseRepository<InvoiceEntity, Invoice>(context, mapper), IInvoiceRepository
    {
        public Task<OperationResult<IEnumerable<InvoiceEntity>>> GetInvoices(CancellationToken cancellationToken)
            => GetAllAsync(cancellationToken);

        public Task<OperationResult<InvoiceEntity?>> GetInvoiceById(Guid id, CancellationToken cancellationToken)
            => GetByIdAsync(id, cancellationToken);

        public Task<OperationResult<InvoiceEntity>> AddInvoice(InvoiceEntity invoice, CancellationToken cancellationToken)
            => AddAsync(invoice, cancellationToken);

        public Task<OperationResult<InvoiceEntity>> UpdateInvoice(InvoiceEntity invoice, CancellationToken cancellationToken)
            => UpdateAsync(invoice, cancellationToken);

        public Task<OperationResult<bool>> DeleteInvoice(Guid id, CancellationToken cancellationToken)
            => DeleteAsync(id, cancellationToken);

        protected override void UpdateEntity(Invoice model, InvoiceEntity domainEntity)
        {
            model.ClientId = domainEntity.ClientId;
            model.CompanyId = domainEntity.CompanyId;
            model.InvoiceDate = domainEntity.InvoiceDate;
            model.Estimate = domainEntity.Estimate;
            model.Signature = domainEntity.Signature;
        }

        protected override Guid GetId(InvoiceEntity domainEntity)
        {
            return domainEntity.InvoiceId;
        }
    }
}
