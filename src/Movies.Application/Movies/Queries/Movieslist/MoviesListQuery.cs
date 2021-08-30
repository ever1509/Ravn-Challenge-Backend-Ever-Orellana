using MediatR;
using Movies.Application.Common.Interfaces;
using System;

namespace Movies.Application.Movies.Queries.Movieslist
{
    public class MoviesListQuery : IRequest<MoviesListVm>, ICacheable
    {        
        public int Page { get; set; } = 1;        
        public int per_page { get; set; } = 4; 
        public int Category { get; set; }       
        public string Title { get; set; }

        public bool UseCache => true;

        public string CacheKey => $"movies-{Category}-{Title}";

        public TimeSpan? SlidingExpiration { get; }
    }
}
