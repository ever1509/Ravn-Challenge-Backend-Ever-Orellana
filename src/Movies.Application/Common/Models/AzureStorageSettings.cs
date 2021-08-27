namespace Movies.Application.Common.Models
{
    public class AzureStorageSettings
    {
        public string ConnectionString { get; set; }
        public string BlobContainer { get; set; }
        public int MaxExpiryTimeSeconds { get; set; }
        public string MenuBlobContainer { get; set; }

    }
}
