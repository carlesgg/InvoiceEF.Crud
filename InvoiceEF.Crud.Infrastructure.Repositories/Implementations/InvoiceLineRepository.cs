using Microsoft.Data.SqlClient;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Infrastructure.Data.Contracts;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class InvoiceLineRepository(ISqlConnectionFactory connectionFactory) : IInvoiceLineRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<InvoiceLine>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<InvoiceLine> invoiceLines = [];
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using SqlCommand command = new(
                "SELECT LineId, InvoiceId, Concept, Quantity, Price, LineTotal FROM InvoiceLines", 
                connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                var invoiceLine = new InvoiceLine
                {
                    LineId = reader.GetInt32(0),
                    InvoiceId = reader.GetInt32(1),
                    Concept = reader.GetString(2),
                    Quantity = reader.GetInt32(3),
                    Price = reader.GetDecimal(4),
                    LineTotal = reader.GetDecimal(5)
                };
                invoiceLines.Add(invoiceLine);
            }

            return await Task.FromResult(invoiceLines.AsEnumerable());
        }

        public async Task<InvoiceLine?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            InvoiceLine? invoiceLine = null;

            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                "SELECT LineId, InvoiceId, Concept, Quantity, Price, LineTotal FROM InvoiceLines WHERE LineId = @Id",
                connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            if (await reader.ReadAsync(cancellationToken))
            {
                invoiceLine = new()
                {
                    LineId = reader.GetInt32(0),
                    InvoiceId = reader.GetInt32(1),
                    Concept = reader.GetString(2),
                    Quantity = reader.GetInt32(3),
                    Price = reader.GetDecimal(4),
                    LineTotal = reader.GetDecimal(5)
                };
            }
            return await Task.FromResult(invoiceLine);
        }

        public async Task<bool> UpdateAsync(InvoiceLine invoiceLine, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                @"UPDATE InvoiceLines 
                  SET InvoiceId = @InvoiceId, 
                      Concept = @Concept, 
                      Quantity = @Quantity, 
                      Price = @Price, 
                      LineTotal = @LineTotal 
                  WHERE LineId = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", invoiceLine.LineId);
            command.Parameters.AddWithValue("@InvoiceId", invoiceLine.InvoiceId);
            command.Parameters.AddWithValue("@Concept", invoiceLine.Concept);
            command.Parameters.AddWithValue("@Quantity", invoiceLine.Quantity);
            command.Parameters.AddWithValue("@Price", invoiceLine.Price);
            command.Parameters.AddWithValue("@LineTotal", invoiceLine.LineTotal);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }

        public async Task<bool> AddAsync(InvoiceLine invoiceLine, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                @"INSERT INTO InvoiceLines 
                  (InvoiceId, Concept, Quantity, Price, LineTotal) 
                  VALUES 
                  (@InvoiceId, @Concept, @Quantity, @Price, @LineTotal)",
                connection);

            command.Parameters.AddWithValue("@InvoiceId", invoiceLine.InvoiceId);
            command.Parameters.AddWithValue("@Concept", invoiceLine.Concept);
            command.Parameters.AddWithValue("@Quantity", invoiceLine.Quantity);
            command.Parameters.AddWithValue("@Price", invoiceLine.Price);
            command.Parameters.AddWithValue("@LineTotal", invoiceLine.LineTotal);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand("DELETE FROM InvoiceLines WHERE LineId = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }
    }
}
