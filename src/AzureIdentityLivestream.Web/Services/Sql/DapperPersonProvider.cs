using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace AzureIdentityLivestream.Web.Services.Sql
{
    public class DapperPersonProvider : IPersonProvider
    {
        private readonly SqlConnectionFactory _sqlConnectionFactory;

        public DapperPersonProvider(SqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            await using var sqlConnection = _sqlConnectionFactory.CreateConnection();
            return await sqlConnection.QueryAsync<Person>("SELECT [FirstName], [LastName], [Age] FROM [dbo].[Person]");
        }
    }
}
