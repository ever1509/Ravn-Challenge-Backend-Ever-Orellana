using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.MovieRates.Queries.RatesByUser
{
    public class RatesByUserQueryHandler : IRequestHandler<RatesByUserQuery, List<RateDto>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMoviesContext _context;
        private readonly IMapper _mapper;
        public RatesByUserQueryHandler(ICurrentUserService currentUserService, IMoviesContext context, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<RateDto>> Handle(RatesByUserQuery request, CancellationToken cancellationToken)
        {
            return await _context.MovieRates.Where(x => x.UserID == _currentUserService.UserId)
                .ProjectTo<RateDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
