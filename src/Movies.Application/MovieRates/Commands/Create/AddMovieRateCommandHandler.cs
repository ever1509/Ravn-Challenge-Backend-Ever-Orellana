using MediatR;
using Movies.Application.Common.Interfaces;
using Movies.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.MovieRates.Commands.Create
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
            var entity = _context.MovieRates.SingleOrDefault(x => x.MovieId == request.Id && x.UserID == _currentUserService.UserId);

            if (entity != null)
                throw new Exception($"Entity \"{nameof(MovieRate)}\" ({request.Id}) already exists.");


            MovieRate movieRate = new MovieRate()
            {
                UserID = _currentUserService.UserId,
                MovieId = request.Id,
                CreatedDate = DateTime.Now
            };

            _context.MovieRates.Add(movieRate);

            await _context.SaveChangesAsync(cancellationToken);

            return movieRate.MovieId;            
        }
    }
}
