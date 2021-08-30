using MediatR;
using Movies.Application.Common.Interfaces;
using System;

namespace Movies.Application.MovieRates.Commands.Create
{
    public class AddMovieRateCommand : IRequest<int>, ICacheable
    {
        public int Id { get; set; }

        public bool UseCache => false;

        public string CacheKey => string.Empty;

        public TimeSpan? SlidingExpiration => null;
    }
}
