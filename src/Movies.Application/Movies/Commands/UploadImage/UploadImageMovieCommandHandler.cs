using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Models.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Movies.Commands.UploadImage
{
    public class UploadImageMovieCommandHandler : IRequestHandler<UploadImageMovieCommand, MediaFileResponse>
    {
        private readonly IMoviesContext _context;
        private readonly IFileService _fileService;
        public UploadImageMovieCommandHandler(IMoviesContext context, IFileService fileService = null)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<MediaFileResponse> Handle(UploadImageMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(e => e.MovieId == request.MovieId);

            if (movie == null)
                throw new Exception($"Not found Movie {request.MovieId}");
           
            var response = await _fileService.UploadFile(request.ImageMovieFileInfo, true);

            movie.Image = response.UploadUri;
            await _context.SaveChangesAsync(cancellationToken);

            return new MediaFileResponse { StringPath = response.UploadUri };
        }
    }
}
