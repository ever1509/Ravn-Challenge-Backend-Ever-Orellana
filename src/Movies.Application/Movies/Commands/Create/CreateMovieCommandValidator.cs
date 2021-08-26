using FluentValidation;

namespace Movies.Application.Movies.Commands.Create
{
    public class CreateMovieCommandValidator: AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(e => e.Title).NotEmpty();
            RuleFor(e => e.Synopsis).NotEmpty();
            RuleFor(e => e.Image).NotEmpty();
            RuleFor(e => e.ReleaseDate).NotEmpty();
            RuleFor(e => e.CategoryId).NotEmpty();            
        }
    }
}
