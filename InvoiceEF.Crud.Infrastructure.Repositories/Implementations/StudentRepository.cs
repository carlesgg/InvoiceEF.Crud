using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceEF.Crud.Domain;
using InvoiceEF.Crud.Infrastructure.Repositories.Contracts;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Implementations
{
    public class StudentRepository(ISqlConnectionFactory connectionFactory) : IStudentRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<StudentTest>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<StudentTest> _students = [];
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);
 
            using SqlCommand command = new("SELECT * FROM StudentTest", connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                var student = new StudentTest
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Direction = reader.GetString(3)
                };
                _students.Add(student);
            }
            
            return await Task.FromResult(_students.AsEnumerable());
        }

        public async Task<StudentTest?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            StudentTest? student = null;

            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand("SELECT Id, Name, Surname, Direction FROM StudentTest WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            if (await reader.ReadAsync(cancellationToken))
            {
                student = new()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Direction = reader.GetString(3)
                };
            }
            return await Task.FromResult(student);
        }

        public async Task<bool> UpdateAsync(StudentTest student, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                "UPDATE StudentTest SET Name = @Name, Surname = @Surname, Direction = @Direction WHERE Id = @Id", connection);

            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Surname", student.Surname);
            command.Parameters.AddWithValue("@Direction", student.Direction);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }

        public async Task<bool> AddAsync(StudentTest student, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand(
                "INSERT INTO StudentTest (Name, Surname, Direction) VALUES (@Name, @Surname, @Direction)", connection);

            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Surname", student.Surname);
            command.Parameters.AddWithValue("@Direction", student.Direction);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {

            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = new SqlCommand("DELETE FROM StudentTest WHERE Id = @Id", connection);

            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            return rowsAffected > 0;
        }
    }
}
