using MediatR;
using Movies.Application.Common.Interfaces;
using Movies.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Movies.Commands.Create
{
    class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
    {
        private readonly IMoviesContext _context;

        public CreateMovieCommandHandler(IMoviesContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = new Movie()
            {
                Title = request.Title,
                Synopsis = request.Synopsis,
                Image = request.Image,
                ReleaseDate = request.ReleaseDate,
                CreatedDate = DateTime.Now,
                CategoryId = request.CategoryId
            };

            _context.Movies.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.MovieId;
        }
    }
}
