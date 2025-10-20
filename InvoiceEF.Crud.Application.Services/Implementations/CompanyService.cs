using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Base.Contracts;
using InvoiceEF.Crud.Infrastructure.Data;
using System.Threading;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork) : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<OperationResult<IEnumerable<CompanyEntity>>> GetCompanies(CancellationToken cancellationToken)
        {
            return await _companyRepository.GetCompanies(cancellationToken);
        }

        public async Task<OperationResult<CompanyEntity?>> GetCompanyById(Guid id, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetCompanyById(id, cancellationToken);
        }

        public async Task<OperationResult<CompanyEntity>> AddCompany(CompanyEntity company, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var addResult = await _companyRepository.UpdateCompany(company, cancellationToken);
                if (addResult.HasErrors)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return addResult;
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return addResult;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<OperationResult<CompanyEntity>> UpdateCompany(CompanyEntity company, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var updateResult = await _companyRepository.UpdateCompany(company, cancellationToken);
                if (updateResult.HasErrors)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return updateResult;
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return updateResult;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<OperationResult<bool>> DeleteCompany(Guid id, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var deleteResult = await _companyRepository.DeleteCompany(id, cancellationToken);
                if (deleteResult.HasErrors)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return deleteResult;
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return deleteResult;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
