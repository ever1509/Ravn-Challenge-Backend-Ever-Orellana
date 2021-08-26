using MediatR;

namespace Movies.Application.Movies.Commands.Delete
{
    public class DeleteMovieCommand : IRequest
    {
        public int MovieId { get; set; }
    }
}
