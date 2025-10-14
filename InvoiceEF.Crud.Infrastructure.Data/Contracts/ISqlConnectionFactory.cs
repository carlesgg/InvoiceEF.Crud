using Microsoft.Data.SqlClient;

namespace InvoiceEF.Crud.Infrastructure.Data.Contracts
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}


