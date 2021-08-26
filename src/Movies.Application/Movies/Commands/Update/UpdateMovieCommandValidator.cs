using FluentValidation;

namespace Movies.Application.Movies.Commands.Update
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(e => e.Title).NotEmpty();
            RuleFor(e => e.Synopsis).NotEmpty();
            RuleFor(e => e.Image).NotEmpty();
            RuleFor(e => e.ReleaseDate).NotEmpty();
            RuleFor(e => e.CategoryId).NotEmpty();
        }
    }
}
