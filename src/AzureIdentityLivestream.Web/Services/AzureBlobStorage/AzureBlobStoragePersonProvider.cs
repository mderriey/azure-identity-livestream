using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace AzureIdentityLivestream.Web.Services.AzureBlobStorage
{
    public class AzureBlobStoragePersonProvider : IPersonProvider
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStoragePersonProvider(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            List<Person> people = new();

            var containerClient = _blobServiceClient.GetBlobContainerClient("people");
            var blobs = containerClient.GetBlobsAsync();
            await foreach (var blob in blobs)
            {
                var blobClient = containerClient.GetBlobClient(blob.Name);

                await using var personStream = new MemoryStream();
                var blobDownloadInfo = await blobClient.DownloadAsync();
                var person = await JsonSerializer.DeserializeAsync<Person>(blobDownloadInfo.Value.Content);

                people.Add(person);
            }

            return people;
        }
    }
}
