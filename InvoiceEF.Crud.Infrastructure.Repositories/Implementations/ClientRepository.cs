using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class ClientRepository(AppDbContext context) : IClientRepository
    {
        private readonly AppDbContext _context = context;

        // Get all clients
        public async Task<OperationResult<IEnumerable<Client>>> GetClients(CancellationToken cancellationToken)
        {
            var clients = await _context.Clients.ToListAsync(cancellationToken);
            return new OperationResult<IEnumerable<Client>>().AddResult(clients);
        }

        // Get client by Id
        public async Task<OperationResult<Client?>> GetClientById(Guid id, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FindAsync([id], cancellationToken);
            return new OperationResult<Client?>().AddResult(client);
        }

        // Add client
        public async Task<OperationResult<Client>> AddClient(Client client, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Client>();

            try
            {
                await _context.Clients.AddAsync(client, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                result.AddResult(client);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }

            return result;
        }

        // Update client
        public async Task<OperationResult<Client>> UpdateClient(Client client, CancellationToken cancellationToken)
        {
            // Fetch existing client first
            var existingResult = await GetClientById(client.ClientId, cancellationToken);

            if (existingResult.Result == null)
            {
                return new OperationResult<Client>().AddError(404, "Client not found");
            }

            var result = new OperationResult<Client>();
            try
            {
                _context.Clients.Update(client);
                await _context.SaveChangesAsync(cancellationToken);
                result.AddResult(client);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }

            return result;
        }

        // Delete client
        public async Task<OperationResult<bool>> DeleteClient(Guid id, CancellationToken cancellationToken)
        {
            // Fetch existing client first
            var existingResult = await GetClientById(id, cancellationToken);

            if (existingResult.Result == null)
            {
                return new OperationResult<bool>().AddError(404, "Client not found");
            }

            var result = new OperationResult<bool>();
            try
            {
                _context.Clients.Remove(existingResult.Result);
                await _context.SaveChangesAsync(cancellationToken);
                result.AddResult(true);
            }
            catch (Exception ex)
            {
                result.AddException(ex);
            }

            return result;
        }
    }
}
