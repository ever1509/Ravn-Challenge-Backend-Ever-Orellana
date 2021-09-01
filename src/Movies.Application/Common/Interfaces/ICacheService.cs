using System;
using System.Threading.Tasks;

namespace Movies.Application.Common.Interfaces
{
    public interface ICacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan? timeToLive = null);
        Task<string> GetCacheResponseAsync(string cacheKey);

        Task FlushCacheAsync(bool forceFlushAll = false);
        Task RemoveElementsContainingAsync(params string[] cacheKeyElements);
    }
}
