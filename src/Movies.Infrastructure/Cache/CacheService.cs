using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Movies.Application.Common.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Movies.Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<CacheService> _logger;
        private readonly RedisCacheSettings _redisOptions;
        private readonly IOptions<JsonSerializerSettings> _options;

        public CacheService(IDistributedCache cache, ILogger<CacheService> logger,
            RedisCacheSettings redisOptions, IOptions<JsonSerializerSettings> options)
        {
            _cache = cache;
            _logger = logger;
            _redisOptions = redisOptions;
            _options = options;
        }

        public async Task FlushCacheAsync(bool forceFlushAll = false)
        {
            var redis = await ConnectionMultiplexer.ConnectAsync(_redisOptions.ConnectionString);
            var serverPort = _redisOptions.ConnectionString.Split(',');
            var server = redis.GetServer(serverPort[0]);
            
            var keys = server.Keys();
            foreach (var key in keys)
            {
                if (forceFlushAll)
                    _cache.Remove(key);
            }
        }
        
        public async Task RemoveElementsContainingAsync(string[] cacheKeyElements)
        {
            var redis = await ConnectionMultiplexer.ConnectAsync(_redisOptions.ConnectionString);
            var serverPort = _redisOptions.ConnectionString.Split(',');
            var server = redis.GetServer(serverPort[0]);
            var keys = server.Keys();
            var criteria = cacheKeyElements.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            if (criteria.Length > 0)
            {
                foreach (var key in keys)
                {
                    if (criteria.All(x => key.ToString().Contains(x)))
                        await _cache.RemoveAsync(key);
                }
            }
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan? timeToLive = null)
        {

            if (!_redisOptions.Enabled)
            {
                _logger.LogWarning("Caching is disabled in settings");
                return;
            }


            if (response == null) return;

            if (timeToLive == null) timeToLive = TimeSpan.FromSeconds(_redisOptions.DefaultCacheDurationSeconds);


            _logger.LogInformation("Starting caching response with key {cacheKey} for {duration}", cacheKey,
                timeToLive.ToString());

            var serializedResponse = JsonConvert.SerializeObject(response, _options.Value);

            await _cache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            }).ConfigureAwait(false);

            _logger.LogInformation("Cache for {cachekey} successfully saved", cacheKey);
        }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            if (!_redisOptions.Enabled)
            {
                _logger.LogWarning("Caching is disabled in settings");
                return null;
            }



            var cacheResponse = await _cache.GetStringAsync(cacheKey).ConfigureAwait(false);

            if (cacheResponse != null)
            {
                _logger.LogInformation("Found Cache response for key {cacheKey}", cacheKey);
            }

            return string.IsNullOrEmpty(cacheResponse) ? null : cacheResponse;
        }
    }
}
