using MediatR;
using Movies.Application.Common.Models.Requests;
using Movies.Application.Common.Models.Responses;

namespace Movies.Application.Movies.Queries.Movieslist
{
    public class MoviesListQuery : PageRequest, IRequest<PagedResponse<MoviesListDto>>
    {

    }
}
