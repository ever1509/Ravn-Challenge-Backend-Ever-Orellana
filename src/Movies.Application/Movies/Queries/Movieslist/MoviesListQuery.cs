using MediatR;

namespace Movies.Application.Movies.Queries.Movieslist
{
    public class MoviesListQuery : IRequest<MoviesListVm>
    {        
        public int Page { get; set; } = 1;        
        public int per_page { get; set; } = 4; 
        public int Category { get; set; }       
        public string Title { get; set; }        
    }
}
