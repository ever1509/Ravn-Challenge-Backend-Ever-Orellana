using MediatR;

namespace Movies.Application.Movies.Commands.Delete
{
    public class DeleteMovieCommand : IRequest
    {
        public int Id { get; set; }
    }
}
