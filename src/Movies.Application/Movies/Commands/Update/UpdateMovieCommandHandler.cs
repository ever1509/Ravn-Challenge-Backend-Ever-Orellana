using MediatR;
using Movies.Application.Common.Interfaces;
using Movies.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Movies.Commands.Update
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
    {
        private readonly IMoviesContext _context;

        public UpdateMovieCommandHandler(IMoviesContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Movies.FindAsync(request.MovieId);

            if (entity == null)
                throw new Exception($"Entity \"{nameof(Movie)}\" ({request.MovieId}) was not found.");

            entity.Title = request.Title;
            entity.Synopsis = request.Synopsis;
            entity.ReleaseDate = request.ReleaseDate;
            entity.Image = request.Image;
            entity.CategoryId = request.CategoryId;            

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
