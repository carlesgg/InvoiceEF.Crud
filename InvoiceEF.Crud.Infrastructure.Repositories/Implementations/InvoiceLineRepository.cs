using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Base.Implementations;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using InvoiceEF.Crud.Infrastructure.Data;
using InvoiceEF.Crud.Infrastructure.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class InvoiceLineRepository(AppDbContext context, IMapper<InvoiceLineEntity, InvoiceLine> mapper) : BaseRepository<InvoiceLineEntity, InvoiceLine>(context, mapper), IInvoiceLineRepository
    {

        public Task<OperationResult<IEnumerable<InvoiceLineEntity>>> GetInvoiceLines(CancellationToken cancellationToken)
            => GetAllAsync(cancellationToken);

        public Task<OperationResult<InvoiceLineEntity?>> GetInvoiceLineById(Guid id, CancellationToken cancellationToken)
            => GetByIdAsync(id, cancellationToken);

        public Task<OperationResult<InvoiceLineEntity>> AddInvoiceLine(InvoiceLineEntity invoiceLine, CancellationToken cancellationToken)
            => AddAsync(invoiceLine, cancellationToken);


        public Task<OperationResult<InvoiceLineEntity>> UpdateInvoiceLine(InvoiceLineEntity invoiceLine, CancellationToken cancellationToken)
            => UpdateAsync(invoiceLine, cancellationToken);

        public Task<OperationResult<bool>> DeleteInvoiceLine(Guid id, CancellationToken cancellationToken)
            => DeleteAsync(id, cancellationToken);

        protected override void UpdateEntity(InvoiceLine model, InvoiceLineEntity domainEntity)
        {
            model.InvoiceId = domainEntity.InvoiceId;
            model.Concept = domainEntity.Concept;
            model.Quantity = domainEntity.Quantity;
            model.Price = domainEntity.Price;
            // model.LineTotal is computed, no need to update
        }

        protected override Guid GetId(InvoiceLineEntity domainEntity)
        {
            return domainEntity.LineId;
        }
    }
}
