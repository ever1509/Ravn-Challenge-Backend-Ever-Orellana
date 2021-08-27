﻿using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.MovieRates.Commands.Create;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [AllowAnonymous]
        public async Task<ActionResult<MoviesListVm>> GetMoviesList([FromQuery] MoviesListQuery request) 
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("create")]
        [Authorize(Roles ="Admin,Test")]
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

        [HttpDelete("delete/{Id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] DeleteMovieCommand request)
        {
            await _mediator.Send(request);

            return NoContent();
        }

        [HttpPost("rate/{Id}")]
        [Authorize]
        public async Task<ActionResult<int>> RateMovie([FromRoute] AddMovieRateCommand command)
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
