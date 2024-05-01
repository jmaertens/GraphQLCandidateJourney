using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.VisualBasic.FileIO;

namespace CandidateJourney.Application.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _blobContainerClient;

        public BlobService(BlobServiceClient blobContainerClient)
        {
            _blobServiceClient = blobContainerClient;
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("candidate-pictures");
        }
        public async Task<string> UploadBlobAsync(byte[] blobContent)
        {
            var blobName = Guid.NewGuid() + ".png";
            BlobContentInfo result;
            var blobClient = _blobContainerClient.GetBlobClient(blobName);
            using (var stream = new MemoryStream(blobContent))
            {
                result = await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = "image/png" });
            }

            return result != null ? blobName : "";
        }

        public Task<string> GetBlobUri(string blobName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(blobName);
            if (!blobClient.ExistsAsync().Result) return Task.FromResult("");
            var imageUri = blobClient.GenerateSasUri(new BlobSasBuilder(
                BlobSasPermissions.Read, DateTimeOffset.Now.AddHours(5))).AbsoluteUri;
            return Task.FromResult(imageUri);
        }
    }
}
