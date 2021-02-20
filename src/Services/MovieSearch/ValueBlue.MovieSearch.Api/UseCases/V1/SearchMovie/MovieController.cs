using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueBlue.MovieSearch.Application.UseCases.SearchMovie;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.SearchMovie
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(MovieSearchByTitleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchMovieByTitleAsync(
            [FromQuery(Name = "t")] [Required] string title)
        {
            var result = await _mediator.Send(new MovieSearchQuery(title));
            return Output.For(result);
        }
    }
}