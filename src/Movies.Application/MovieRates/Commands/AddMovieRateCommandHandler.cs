using MediatR;
using Movies.Application.Common.Interfaces;
using Movies.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.MovieRates.Commands
{
    public class AddMovieRateCommandHandler : IRequestHandler<AddMovieRateCommand, int>
    {
        private readonly IMoviesContext _context;
        private readonly ICurrentUserService _currentUserService;
        public AddMovieRateCommandHandler(IMoviesContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(AddMovieRateCommand request, CancellationToken cancellationToken)
        {
            request.UserId = _currentUserService.UserId;
            var entity = _context.MovieRates.SingleOrDefault(x=>x.MovieId==request.MovieId && x.UserId == request.UserId);

            if (entity == null)
                throw new Exception($"Entity \"{nameof(MovieRate)}\" ({request.MovieId}) already exists.");


            MovieRate movieRate = new MovieRate()
            {               
                UserId = request.UserId,
                MovieId = request.MovieId,
                CreatedDate = DateTime.Now
            };

            _context.MovieRates.Add(movieRate);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.MovieId;

        }
    }
}
