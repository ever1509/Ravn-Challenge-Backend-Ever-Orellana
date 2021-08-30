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
        private readonly IMoviesContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        public RatesByUserQueryHandler(IMoviesContext context, IMapper mapper, IIdentityService identityService)
        {            
            _context = context;
            _mapper = mapper;
            _identityService = identityService;
        }


        public async Task<List<UserDto>> Handle(RatesByUserQuery request, CancellationToken cancellationToken)
        {
            var userDtoList = new List<UserDto>();
            var userIds = await _context.MovieRates.Select(x => x.UserID).Distinct().ToArrayAsync();

            foreach(var id in userIds)
            {
                var userDto = new UserDto();
                var user = await _identityService.GetUserNameAsync(id);
                userDto.UserName = user;
                userDto.RatedMovies = await _context.MovieRates.Where(x => x.UserID == id).ProjectTo<RateDto>(_mapper.ConfigurationProvider).ToListAsync();
                userDtoList.Add(userDto);
            }

            return userDtoList;
        }
    }
}
