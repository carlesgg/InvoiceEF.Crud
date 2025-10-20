using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Base.Contracts;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork) : IClientService
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<OperationResult<IEnumerable<ClientEntity>>> GetClients(CancellationToken cancellationToken)
        {
            return await _clientRepository.GetClients(cancellationToken);
        }

        public async Task<OperationResult<ClientEntity?>> GetClientById(Guid id, CancellationToken cancellationToken)
        {
            return await _clientRepository.GetClientById(id, cancellationToken);
        }

        public async Task<OperationResult<ClientEntity>> AddClient(ClientEntity client, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var addResult = await _clientRepository.AddClient(client, cancellationToken);
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

        public async Task<OperationResult<ClientEntity>> UpdateClient(ClientEntity client, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var updateResult = await _clientRepository.AddClient(client, cancellationToken);
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

        public async Task<OperationResult<bool>> DeleteClient(Guid id, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var deleteResult = await _clientRepository.DeleteClient(id, cancellationToken);
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
