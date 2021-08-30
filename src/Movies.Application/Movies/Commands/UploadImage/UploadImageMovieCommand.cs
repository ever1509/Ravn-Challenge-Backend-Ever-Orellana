using MediatR;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Models.Requests;
using Movies.Application.Common.Models.Responses;
using System;

namespace Movies.Application.Movies.Commands.UploadImage
{
    public class UploadImageMovieCommand:IRequest<MediaFileResponse>, ICacheable
    {
        public MediaFileRequest ImageMovieFileInfo { get; set; }
        public int MovieId { get; set; }

        public bool UseCache => false;

        public string CacheKey => string.Empty;

        public TimeSpan? SlidingExpiration => null;
    }
}
