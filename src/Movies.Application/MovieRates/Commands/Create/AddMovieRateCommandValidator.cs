using FluentValidation;

namespace Movies.Application.MovieRates.Commands.Create
{
    public class AddMovieRateCommandValidator : AbstractValidator<AddMovieRateCommand>
    {
        public AddMovieRateCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
        }
    }
}
