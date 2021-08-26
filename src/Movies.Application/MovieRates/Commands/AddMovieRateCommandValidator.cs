using FluentValidation;

namespace Movies.Application.MovieRates.Commands
{
    public class AddMovieRateCommandValidator : AbstractValidator<AddMovieRateCommand>
    {
        public AddMovieRateCommandValidator()
        {
            RuleFor(e => e.MovieId).NotEmpty();
        }
    }
}
