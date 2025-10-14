using Microsoft.Data.SqlClient;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Infrastructure.Data.Contracts;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class ClientRepository(ISqlConnectionFactory connectionFactory) : IClientRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<Client> _clients = [];
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);
 
            using SqlCommand command = new("SELECT * FROM Clients", connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                var client = new Client
                {
                    ClientId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Direction = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4)
                };
                _clients.Add(client);
            }
            
            return await Task.FromResult(_clients.AsEnumerable());
        }

        public async Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            Client? client = null;

            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                "SELECT ClientId, Name, Direction, Email, Phone FROM Clients WHERE ClientId = @Id", 
                connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            if (await reader.ReadAsync(cancellationToken))
            {
                client = new()
                {
                    ClientId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Direction = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4)
                };
            }
            return await Task.FromResult(client);
        }

        public async Task<bool> UpdateAsync(Client client, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                "UPDATE Clients SET Name = @Name, Direction = @Direction, Email = @Email, Phone = @Phone WHERE ClientId = @Id", 
                connection);

            command.Parameters.AddWithValue("@Id", client.ClientId);
            command.Parameters.AddWithValue("@Name", client.Name);
            command.Parameters.AddWithValue("@Direction", client.Direction);
            command.Parameters.AddWithValue("@Email", client.Email);
            command.Parameters.AddWithValue("@Phone", client.Phone);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }

        public async Task<bool> AddAsync(Client client, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                "INSERT INTO Clients (Name, Direction, Email, Phone) VALUES (@Name, @Direction, @Email, @Phone)", 
                connection);

            command.Parameters.AddWithValue("@Name", client.Name);
            command.Parameters.AddWithValue("@Direction", client.Direction);
            command.Parameters.AddWithValue("@Email", client.Email);
            command.Parameters.AddWithValue("@Phone", client.Phone);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand("DELETE FROM Clients WHERE ClientId = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }
    }
}
