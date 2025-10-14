using Microsoft.Data.SqlClient;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Infrastructure.Data.Contracts;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class CompanyRepository(ISqlConnectionFactory connectionFactory) : ICompanyRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Company>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<Company> companies = [];
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);
 
            using SqlCommand command = new("SELECT CompanyId, Name, Direction, Email, Phone FROM Companies", connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                var company = new Company
                {
                    CompanyId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Direction = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4)
                };
                companies.Add(company);
            }
            
            return await Task.FromResult(companies.AsEnumerable());
        }

        public async Task<Company?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            Company? company = null;

            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                "SELECT CompanyId, Name, Direction, Email, Phone FROM Companies WHERE CompanyId = @Id", 
                connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            if (await reader.ReadAsync(cancellationToken))
            {
                company = new()
                {
                    CompanyId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Direction = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4)
                };
            }
            return await Task.FromResult(company);
        }

        public async Task<bool> UpdateAsync(Company company, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                "UPDATE Companies SET Name = @Name, Direction = @Direction, Email = @Email, Phone = @Phone WHERE CompanyId = @Id", 
                connection);

            command.Parameters.AddWithValue("@Id", company.CompanyId);
            command.Parameters.AddWithValue("@Name", company.Name);
            command.Parameters.AddWithValue("@Direction", company.Direction);
            command.Parameters.AddWithValue("@Email", company.Email);
            command.Parameters.AddWithValue("@Phone", company.Phone);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }

        public async Task<bool> AddAsync(Company company, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                "INSERT INTO Companies (Name, Direction, Email, Phone) VALUES (@Name, @Direction, @Email, @Phone)", 
                connection);

            command.Parameters.AddWithValue("@Name", company.Name);
            command.Parameters.AddWithValue("@Direction", company.Direction);
            command.Parameters.AddWithValue("@Email", company.Email);
            command.Parameters.AddWithValue("@Phone", company.Phone);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand("DELETE FROM Companies WHERE CompanyId = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }
    }
}
