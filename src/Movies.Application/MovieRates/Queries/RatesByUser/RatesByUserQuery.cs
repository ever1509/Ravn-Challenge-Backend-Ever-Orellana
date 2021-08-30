using MediatR;
using Movies.Application.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Movies.Application.MovieRates.Queries.RatesByUser
{
    public class RatesByUserQuery : IRequest<List<UserDto>>, ICacheable
    {
        public bool UseCache => false;

        public string CacheKey => string.Empty;

        public TimeSpan? SlidingExpiration => null;
    }
}
