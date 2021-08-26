using MediatR;
using Movies.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Movies.Commands.Delete
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IMoviesContext _context;

        public DeleteMovieCommandHandler(IMoviesContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Movies.FindAsync(request.MovieId);

            if (entity == null)
                throw new Exception($"Entity \"{nameof(Movies)}\" ({request.MovieId}) was not found.");

            _context.Movies.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
