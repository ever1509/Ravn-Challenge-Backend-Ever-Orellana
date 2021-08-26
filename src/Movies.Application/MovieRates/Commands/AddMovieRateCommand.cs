using MediatR;

namespace Movies.Application.MovieRates.Commands
{
    public class AddMovieRateCommand : IRequest<int>
    {
        public int MovieId { get; set; }
        public string UserId { get; set; }
    }
}
