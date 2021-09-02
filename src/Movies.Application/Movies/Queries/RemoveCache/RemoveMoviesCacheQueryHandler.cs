using MediatR;
using Movies.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Movies.Queries.RemoveCache
{
    public class RemoveMoviesCacheQueryHandler : IRequestHandler<RemoveMoviesCacheQuery>
    {
        private readonly ICacheService _cacheService;

        public RemoveMoviesCacheQueryHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Unit> Handle(RemoveMoviesCacheQuery request, CancellationToken cancellationToken)
        {
            await _cacheService.FlushCacheAsync(true);
            return Unit.Value;
        }
    }
}
