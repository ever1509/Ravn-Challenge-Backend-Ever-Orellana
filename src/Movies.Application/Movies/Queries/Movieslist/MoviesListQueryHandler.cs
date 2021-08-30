using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Movies.Queries.Movieslist
{
    public class MoviesListQueryHandler : IRequestHandler<MoviesListQuery, MoviesListVm>
    {
        private readonly IMoviesContext _context;
        public IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public MoviesListQueryHandler(IMoviesContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<MoviesListVm> Handle(MoviesListQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"allmovies-{request.Category}-{request.Title}-{request.Page}-{request.per_page}";            
            var response = new MoviesListVm();

            if (!_memoryCache.TryGetValue(cacheKey, out response))
            {
                List<MovieDto> movies;

                if (request.Category > 0 && !(string.IsNullOrEmpty(request.Title)))
                {
                    movies = await _context.Movies.Where(x => x.CategoryId == request.Category && x.Title.Contains(request.Title))
                                     .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
                }
                else if (request.Category == 0 && !(string.IsNullOrEmpty(request.Title)))
                {
                    movies = await _context.Movies.Where(x => x.Title.Contains(request.Title))
                                     .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
                }
                else if (request.Category > 0 && string.IsNullOrEmpty(request.Title))
                {
                    movies = await _context.Movies.Where(x => x.CategoryId == request.Category)
                                     .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
                }
                else
                {
                    movies = await _context.Movies.ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
                }

                PagedResponse<MovieDto> pagedMovies = BuildPagination(movies, request.Page, request.per_page);
                var moviesListVm = new MoviesListVm { Movies = pagedMovies };

                _memoryCache.Set(cacheKey, moviesListVm, new MemoryCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddHours(6), Priority = CacheItemPriority.Normal, SlidingExpiration = TimeSpan.FromMinutes(5) });

                return moviesListVm;
            }

            return response;
               
        }

        private PagedResponse<MovieDto> BuildPagination(List<MovieDto> movies, int? page, int? pageSize)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalm = movies.Count();

            movies = movies.Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize).ToList();

            return new PagedResponse<MovieDto>
            {
                RowCount = totalm,
                CurrentPage = currentPage,
                PageCount = (int)Math.Ceiling((decimal)totalm / currentPageSize),
                Items = movies.Skip((currentPage - 1) * currentPageSize).Take(currentPageSize).ToList(),
                PageSize = currentPageSize
            };
        }
    }
}
