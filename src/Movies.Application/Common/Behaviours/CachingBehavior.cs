using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Models;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Common.Behaviours
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheable
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;
        public CachingBehavior(IMemoryCache cache, ILogger<TResponse> logger)
        {
            _cache = cache;
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType();
            _logger.LogInformation("{Request} is configured for caching", requestName);

            TResponse response;
            if (_cache.TryGetValue(request.CacheKey, out response))
            {
                _logger.LogInformation("Returning cached value for {Request}", requestName);
                return response;
            }
            _logger.LogInformation("{Request} Cache Key: {Key} is not inside the cache, executing request.", requestName, request.CacheKey);

            response = await next();
            _cache.Set(request.CacheKey, response);

            return response;
        }
    }
}
