using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AzureIdentityLivestream.Web.Services.Sql
{
    public class AzureAdAuthenticationDbConnectionInterceptor : DbConnectionInterceptor
    {
        private readonly IAzureSqlTokenProvider _azureSqlTokenProvider;

        public AzureAdAuthenticationDbConnectionInterceptor(IAzureSqlTokenProvider azureSqlTokenProvider)
        {
            _azureSqlTokenProvider = azureSqlTokenProvider;
        }

        public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            var sqlConnection = (SqlConnection)connection;
            if (ConnectionNeedsAccessToken(sqlConnection))
            {
                var (token, _) = _azureSqlTokenProvider.GetAccessToken();
                sqlConnection.AccessToken = token;
            }

            return base.ConnectionOpening(connection, eventData, result);
        }

        public override async ValueTask<InterceptionResult> ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        {
            var sqlConnection = (SqlConnection)connection;
            if (ConnectionNeedsAccessToken(sqlConnection))
            {
                var (token, _) = await _azureSqlTokenProvider.GetAccessTokenAsync(cancellationToken);
                sqlConnection.AccessToken = token;
            }

            return await base.ConnectionOpeningAsync(connection, eventData, result, cancellationToken);
        }

        private static bool ConnectionNeedsAccessToken(SqlConnection connection)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connection.ConnectionString);

            return connectionStringBuilder.DataSource.Contains("database.windows.net", StringComparison.OrdinalIgnoreCase) &&
                string.IsNullOrEmpty(connectionStringBuilder.UserID);
        }
    }
}
