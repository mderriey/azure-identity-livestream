using System;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
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

        public async Task<SqlConnection> CreateConnection()
        {
            var sqlConnection = new SqlConnection(_connectionString);

            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            if (sqlConnection.DataSource.Contains("database.windows.net", StringComparison.OrdinalIgnoreCase) &&
                string.IsNullOrEmpty(sqlConnectionStringBuilder.UserID))
            {
                var credential = new ChainedTokenCredential(
                    new ManagedIdentityCredential(),
                    new VisualStudioCodeCredential());

                var tokenRequest = new TokenRequestContext(new[] { "https://database.windows.net//.default" });
                var tokenResponse = await credential.GetTokenAsync(tokenRequest);

                sqlConnection.AccessToken = tokenResponse.Token;
            }

            return sqlConnection;
        }
    }
}
