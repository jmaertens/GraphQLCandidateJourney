using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace CandidateJourney.Application.Services
{
    public interface IBlobService
    {
        Task<string> UploadBlobAsync(byte[] blobContent);
        Task<string> GetBlobUri(string blobName);
    }
}
