using MediatR;
using Movies.Application.Common.Interfaces;
using System;

namespace Movies.Application.Movies.Commands.Create
{
    public class CreateMovieCommand : IRequest<int>, ICacheable
    {
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }

        public bool UseCache => false;

        public string CacheKey => string.Empty;

        public TimeSpan? SlidingExpiration => null;
    }
}
