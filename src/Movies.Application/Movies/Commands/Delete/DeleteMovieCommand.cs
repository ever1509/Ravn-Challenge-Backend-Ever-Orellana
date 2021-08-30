using MediatR;
using Movies.Application.Common.Interfaces;
using System;

namespace Movies.Application.Movies.Commands.Delete
{
    public class DeleteMovieCommand : IRequest, ICacheable
    {
        public int Id { get; set; }

        public bool UseCache => false;

        public string CacheKey => string.Empty;

        public TimeSpan? SlidingExpiration => null;
    }
}
