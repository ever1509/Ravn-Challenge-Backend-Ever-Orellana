using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Movies.Queries.Movieslist;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/movies/")]
    [ApiController]
    [EnableCors("MoviesCatalog")]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<MoviesListVm>> GetMoviesList([FromQuery] MoviesListQuery request) 
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
