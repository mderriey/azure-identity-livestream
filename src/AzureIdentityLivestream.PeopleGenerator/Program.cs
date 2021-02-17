using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AzureIdentityLivestream.PeopleGenerator
{
    public class Program
    {
        public static async Task Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            var people = await PersonCreator.CreateRandomPeople(30);
            await SqlPeopleGenerator.CreatePeople(configuration, people);
        }
    }
}
