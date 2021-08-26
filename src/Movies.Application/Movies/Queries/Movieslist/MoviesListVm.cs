using Movies.Application.Common.Models.Responses;

namespace Movies.Application.Movies.Queries.Movieslist
{
    public class MoviesListVm
    {
        public PagedResponse<MovieDto> Movies { get; set; }
    }
}
