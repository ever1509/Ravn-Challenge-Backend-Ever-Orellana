using FluentValidation;

namespace Movies.Application.Movies.Commands.Delete
{
    public class DeleteMovieCommandValidator: AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(e => e.MovieId).NotEmpty();            
        }
    }
}
