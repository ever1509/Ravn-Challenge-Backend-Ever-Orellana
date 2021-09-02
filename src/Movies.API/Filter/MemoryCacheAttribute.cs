using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Movies.API.Extensions;
using Movies.Application.Common.Interfaces;
using Movies.Application.Movies.Queries.Movieslist;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Movies.API.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MemoryCacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int? _cacheDuration;
        public MemoryCacheAttribute()
        {

        }
        public MemoryCacheAttribute(int cacheDuration)
        {
            _cacheDuration = cacheDuration;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var request = context.HttpContext.Request;

                var query_params = request.Query.ToDictionary(x=>x.Key.ToString(), x=>x.Value.ToString());

                var redisCache = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
                var key = request.GenerateCacheKey(query_params);
                var cacheResponse = await redisCache.GetCacheResponseAsync(key);

                if (!string.IsNullOrEmpty(cacheResponse))
                {
                    var contentResult = new ContentResult()
                    {
                        ContentType = "application/json",
                        Content = cacheResponse,
                        StatusCode = (int?)HttpStatusCode.OK
                    };

                    context.Result = contentResult;
                    return;
                }

                var executedContext = await next();

                if (executedContext.Result is ObjectResult result)
                {
                    var cacheDuration = (_cacheDuration.HasValue) ? TimeSpan.FromSeconds(_cacheDuration.Value) : (TimeSpan?)null;
                    await redisCache.CacheResponseAsync(key, result.Value, cacheDuration);
                }

            }
            catch (Exception)
            {
                await next();
            }
        }
    }
}
