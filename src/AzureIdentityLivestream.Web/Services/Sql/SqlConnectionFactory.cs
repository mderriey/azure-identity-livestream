using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace AzureIdentityLivestream.Web.Services.Sql
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;
        private readonly IAzureSqlTokenProvider _azureSqlTokenProvider;

        public SqlConnectionFactory(string connectionString, IAzureSqlTokenProvider azureSqlTokenProvider)
        {
            _connectionString = connectionString;
            _azureSqlTokenProvider = azureSqlTokenProvider;
        }

        public async Task<SqlConnection> CreateConnection()
        {
            var sqlConnection = new SqlConnection(_connectionString);

            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            if (sqlConnection.DataSource.Contains("database.windows.net", StringComparison.OrdinalIgnoreCase) &&
                string.IsNullOrEmpty(sqlConnectionStringBuilder.UserID))
            {
                var (token, _) = await _azureSqlTokenProvider.GetAccessTokenAsync();
                sqlConnection.AccessToken = token;
            }

            return sqlConnection;
        }
    }
}
