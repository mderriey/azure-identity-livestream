using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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

            var blobServiceClient = new BlobServiceClient(configuration.GetValue<string>("StorageConnectionString"));

            var containerClient = blobServiceClient.GetBlobContainerClient("people");
            await containerClient.CreateIfNotExistsAsync();

            var people = await PersonCreator.CreateRandomPeople(30);
            foreach (var person in people)
            {
                var blobName = $"{Guid.NewGuid()}.json";
                var blobClient = containerClient.GetBlobClient(blobName);

                await using var jsonStream = new MemoryStream();
                await JsonSerializer.SerializeAsync(jsonStream, person);

                jsonStream.Position = 0;
                await blobClient.UploadAsync(
                    jsonStream,
                    new BlobUploadOptions
                    {
                        HttpHeaders = new BlobHttpHeaders
                        {
                            ContentType = "application/json"
                        }
                    });
            }
        }
    }
}
