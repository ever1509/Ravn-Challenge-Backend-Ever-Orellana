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
    public class RatesByUserQueryHandler : IRequestHandler<RatesByUserQuery, List<UserDto>>
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


        public async Task<List<UserDto>> Handle(RatesByUserQuery request, CancellationToken cancellationToken)
        {
            var userDtoList = new List<UserDto>();
            var userIds = await _context.MovieRates.Select(x => x.UserID).Distinct().ToArrayAsync();

            foreach(var id in userIds)
            {
                var userDto = new UserDto();
                var movies = await _context.MovieRates.Where(x => x.UserID == id).ProjectTo<RateDto>(_mapper.ConfigurationProvider).ToListAsync();
                userDto.UserId = id;
                userDto.RatedMovies = movies;
                userDtoList.Add(userDto);
            }

            return userDtoList;
        }
    }
}
