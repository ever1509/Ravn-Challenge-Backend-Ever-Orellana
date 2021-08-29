using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Models;
using Movies.Application.Common.Models.Requests;
using Movies.Application.Common.Models.Responses;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Movies.Infrastructure.File
{
    public class AzureFileService : IFileService
    {
        private readonly ILogger<AzureFileService> _logger;
        private CloudStorageAccount _storageAccount;
        private IOptions<AzureStorageSettings> _azureSettings;

        public AzureFileService(IOptions<AzureStorageSettings> azureSettings, ILogger<AzureFileService> logger)
        {
            _azureSettings = azureSettings;
            _storageAccount = CloudStorageAccount.Parse(_azureSettings.Value.ConnectionString);
            _logger = logger;
        }
        
        public async Task<UploadCompleteResponse> UploadFile(MediaFileRequest data)
        {
            _logger.LogInformation("Begining upload to Azure for file {filename} with size {size}", data.FileName, data.FileData.Length);

            var blobClient = _storageAccount.CreateCloudBlobClient();
            Stream dataStream = new MemoryStream();
            await data.FileData.CopyToAsync(dataStream);
            using (dataStream)
            {
                dataStream.Seek(0, SeekOrigin.Begin);

                CloudBlobContainer container;
                container = await GetContainerReferenceAsync(blobClient);

                var fileName = GetFileName(data);
                var blobReference = container.GetBlockBlobReference(fileName);
                await blobReference.UploadFromStreamAsync(dataStream);
                blobReference.Properties.ContentType = data.ContentType;
                await blobReference.SetPropertiesAsync();

                _logger.LogInformation("Upload complete for file {filename}. File Uri {uri}", data.FileName, blobReference.Uri.AbsoluteUri);
                return new UploadCompleteResponse()
                {
                    ContentType = blobReference.Properties.ContentType,
                    UploadUri = blobReference.Uri.AbsoluteUri,
                    SavedUri = blobReference.Uri.AbsoluteUri,
                    Container = "Azure",
                    GenerateName = fileName,
                    FileSize = blobReference.Properties.Length
                };
            }
        }

        private async Task<CloudBlobContainer> GetContainerReferenceAsync(CloudBlobClient blobClient)
        {
            var container = blobClient.GetContainerReference(_azureSettings.Value.MoviesContainer);
            await container.CreateIfNotExistsAsync().ConfigureAwait(false);
            return container;
        }
       
        private string GetFileName(MediaFileRequest data)
        {

            var ext = Path.GetExtension(data.FileName);
            var filename = $"{Guid.NewGuid().ToString()}{ext.ToLower()}";

            if (!string.IsNullOrEmpty(data.Folder)) filename = Path.Combine(data.Folder, filename).ToLower();
            return filename;
        }

        public async Task<byte[]> DownloadFile(string name)
        {
            var blobClient = _storageAccount.CreateCloudBlobClient();
            var container = await GetContainerReferenceAsync(blobClient);
            var newBlob = container.GetBlockBlobReference(new CloudBlockBlob(new Uri(name)).Name);
            byte[] fileBytes = null;

            using (var dataStream = new MemoryStream())
            {
                await newBlob.DownloadToStreamAsync(dataStream);
                fileBytes = dataStream.ToArray();
            }

            return fileBytes;
        }
    }
}
