using Microsoft.Data.SqlClient;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Infrastructure.Data.Contracts;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class InvoiceRepository(ISqlConnectionFactory connectionFactory) : IInvoiceRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Invoice>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<Invoice> invoices = [];
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);
 
            using SqlCommand command = new(
                "SELECT InvoiceId, ClientId, CompanyId, InvoiceDate, Estimate, Signature FROM Invoices", 
                connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                var invoice = new Invoice
                {
                    InvoiceId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    CompanyId = reader.GetInt32(2),
                    InvoiceDate = reader.GetDateTime(3),
                    Estimate = reader.GetDecimal(4),
                    Signature = reader.GetString(5)
                };
                invoices.Add(invoice);
            }
            
            return await Task.FromResult(invoices.AsEnumerable());
        }

        public async Task<Invoice?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            Invoice? invoice = null;

            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                "SELECT InvoiceId, ClientId, CompanyId, InvoiceDate, Estimate, Signature FROM Invoices WHERE InvoiceId = @Id", 
                connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            if (await reader.ReadAsync(cancellationToken))
            {
                invoice = new()
                {
                    InvoiceId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    CompanyId = reader.GetInt32(2),
                    InvoiceDate = reader.GetDateTime(3),
                    Estimate = reader.GetDecimal(4),
                    Signature = reader.GetString(5)
                };
            }
            return await Task.FromResult(invoice);
        }

        public async Task<bool> UpdateAsync(Invoice invoice, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                @"UPDATE Invoices 
                SET ClientId = @ClientId, 
                    CompanyId = @CompanyId, 
                    InvoiceDate = @InvoiceDate, 
                    Estimate = @Estimate, 
                    Signature = @Signature 
                WHERE InvoiceId = @Id", 
                connection);

            command.Parameters.AddWithValue("@Id", invoice.InvoiceId);
            command.Parameters.AddWithValue("@ClientId", invoice.ClientId);
            command.Parameters.AddWithValue("@CompanyId", invoice.CompanyId);
            command.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
            command.Parameters.AddWithValue("@Estimate", invoice.Estimate);
            command.Parameters.AddWithValue("@Signature", invoice.Signature);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }

        public async Task<bool> AddAsync(Invoice invoice, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                @"INSERT INTO Invoices 
                (ClientId, CompanyId, InvoiceDate, Estimate, Signature) 
                VALUES 
                (@ClientId, @CompanyId, @InvoiceDate, @Estimate, @Signature)", 
                connection);

            command.Parameters.AddWithValue("@ClientId", invoice.ClientId);
            command.Parameters.AddWithValue("@CompanyId", invoice.CompanyId);
            command.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
            command.Parameters.AddWithValue("@Estimate", invoice.Estimate);
            command.Parameters.AddWithValue("@Signature", invoice.Signature);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand("DELETE FROM Invoices WHERE InvoiceId = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }
    }
}
