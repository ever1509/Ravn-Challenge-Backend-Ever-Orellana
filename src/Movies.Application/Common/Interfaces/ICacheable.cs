using System;

namespace Movies.Application.Common.Interfaces
{
    public interface ICacheable
    {
        bool UseCache { get; }
        string CacheKey { get; }
        TimeSpan? SlidingExpiration { get; }
    }
}
