using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Application.Movies.Commands.UploadImage
{
    public class UploadImageMoveCommandValidator: AbstractValidator<UploadImageMovieCommand>
    {
        public UploadImageMoveCommandValidator()
        {
            RuleFor(e => e.ImageMovieFileInfo).NotNull();
            RuleFor(e => e.MovieId).NotEmpty();
        }
    }
}
