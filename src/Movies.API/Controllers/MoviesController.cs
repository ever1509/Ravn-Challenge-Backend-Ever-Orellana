using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.MovieRates.Commands;
using Movies.Application.MovieRates.Queries.RatesByUser;
using Movies.Application.Movies.Commands.Create;
using Movies.Application.Movies.Commands.Delete;
using Movies.Application.Movies.Commands.Update;
using Movies.Application.Movies.Queries.Movieslist;
using System.Collections.Generic;
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

        [HttpPost("create")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<int>> Create([FromBody] CreateMovieCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] UpdateMovieCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteMovieCommand { MovieId = id });

            return NoContent();
        }

        [HttpPost("rate-movie")]
        [Authorize]
        public async Task<ActionResult<int>> AddLike([FromBody] AddMovieRateCommand command)
        {
            var movieId = await _mediator.Send(command);

            return Ok(movieId);
        }

        [HttpGet("rates-by-user")]
        [Authorize]
        public async Task<ActionResult<List<RateDto>>> GetRatesByUser([FromBody] RatesByUserQuery command)
        {
            var movieId = await _mediator.Send(command);

            return Ok(movieId);
        }
    }
}
