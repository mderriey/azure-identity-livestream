using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureIdentityLivestream.PeopleGenerator
{
    public static class PersonCreator
    {
        public static async Task<IReadOnlyList<Person>> CreateRandomPeople(int numberOfPeople)
        {
            await using var firstNamesStream = File.OpenRead("first-names.json");
            var firstNamesDictionary = await JsonSerializer.DeserializeAsync<Dictionary<string, string[]>>(firstNamesStream);

            await using var lastNamesStream = File.OpenRead("last-names.json");
            var lastNamesDictionary = await JsonSerializer.DeserializeAsync<Dictionary<string, string[]>>(lastNamesStream);

            return Enumerable.Range(1, numberOfPeople)
                .Select(x =>
                {
                    var initialIndex = RandomNumberGenerator.GetInt32(firstNamesDictionary.Keys.Count);
                    var initial = firstNamesDictionary.Keys.ElementAt(initialIndex);

                    var firstNames = firstNamesDictionary[initial];
                    var lastNames = lastNamesDictionary[initial];

                    return new Person(
                         firstNames[RandomNumberGenerator.GetInt32(firstNames.Length)],
                         lastNames[RandomNumberGenerator.GetInt32(lastNames.Length)],
                         RandomNumberGenerator.GetInt32(5, 101));
                })
                .ToList()
                .AsReadOnly();
        }
    }
}
