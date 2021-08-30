using MediatR;
using System.Collections.Generic;

namespace Movies.Application.MovieRates.Queries.RatesByUser
{
    public class RatesByUserQuery : IRequest<List<UserDto>>
    {      
    }
}
