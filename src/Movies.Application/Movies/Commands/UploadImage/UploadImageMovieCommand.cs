using MediatR;
using Movies.Application.Common.Models.Requests;
using Movies.Application.Common.Models.Responses;

namespace Movies.Application.Movies.Commands.UploadImage
{
    public class UploadImageMovieCommand:IRequest<MediaFileResponse>
    {
        public MediaFileRequest ImageMovieFileInfo { get; set; }
        public int MovieId { get; set; }
    }
}
