namespace Movies.Application.Common.Models.Responses
{
    public class UploadCompleteResponse
    {
        public string Container { get; set; }
        public string UploadUri { get; set; }
        public string GenerateName { get; set; }
        public string ContentType { get; set; }
        public long? FileSize { get; set; }
        public string SavedUri { get; set; }
    }
}
