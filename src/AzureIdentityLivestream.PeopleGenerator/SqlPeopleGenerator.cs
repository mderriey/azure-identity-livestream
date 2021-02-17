using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AzureIdentityLivestream.PeopleGenerator
{
    public static class SqlPeopleGenerator
    {
        public static async Task CreatePeople(IConfiguration configuration, IReadOnlyList<Person> people)
        {
            await using var sqlConnection = new SqlConnection(configuration.GetValue<string>("SqlConnectionString"));

            foreach (var person in people)
            {
                await sqlConnection.ExecuteScalarAsync(
                    "INSERT INTO [dbo].[Person] ([FirstName], [LastName], [Age]) VALUES (@firstName, @lastName, @age)",
                    new { person.FirstName, person.LastName, person.Age });
            }
        }
    }
}
