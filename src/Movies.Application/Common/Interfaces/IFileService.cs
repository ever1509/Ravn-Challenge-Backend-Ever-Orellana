using Movies.Application.Common.Models.Requests;
using Movies.Application.Common.Models.Responses;
using System.Threading.Tasks;

namespace Movies.Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<UploadCompleteResponse> UploadFileBySas(MediaFileRequest data);
        Task<UploadCompleteResponse> UploadFile(MediaFileRequest fileRequestDto, bool menuContainer);
        Task<string> GetSelfSignedSignature();
        Task<byte[]> DownloadFile(string name);
    }
}
