using MediatR;

namespace Movies.Application.MovieRates.Commands.Create
{
    public class AddMovieRateCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
