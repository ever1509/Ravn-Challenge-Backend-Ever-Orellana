using MediatR;
using Movies.Application.Common.Models.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Movies.Queries.Movieslist
{
    public class MoviesListQueryHandler : IRequestHandler<MoviesListQuery, PagedResponse<MoviesListDto>>
    {
        public Task<PagedResponse<MoviesListDto>> Handle(MoviesListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
