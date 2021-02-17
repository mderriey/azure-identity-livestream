using Microsoft.Data.SqlClient;

namespace AzureIdentityLivestream.Web.Services.Sql
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
