using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using InvoiceEF.Crud.Infrastructure.Data.Contracts;

namespace InvoiceEF.Crud.Infrastructure.Context
{
    public class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
    {
        private readonly string _connectionString = configuration.GetConnectionString("InvoiceExamEFDb")
                ?? throw new InvalidOperationException("Connection string 'InvoiceExamEFDb' not found.");

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
