using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Application.MovieRates.Queries.RatesByUser
{
    public class UserDto
    {
        public string UserName { get; set; }
        public List<RateDto> RatedMovies { get; set; }
    }
}
