using Movies.Application.Common.Models.Requests;
using Movies.Application.Common.Models.Responses;
using System.Threading.Tasks;

namespace Movies.Application.Common.Interfaces
{
    public interface IFileService
    {      
        Task<UploadCompleteResponse> UploadFile(MediaFileRequest fileRequestDto);     
        Task<byte[]> DownloadFile(string name);
    }
}
