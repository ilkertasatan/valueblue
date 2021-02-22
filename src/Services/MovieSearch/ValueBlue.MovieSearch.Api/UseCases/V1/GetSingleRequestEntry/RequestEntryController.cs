using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueBlue.MovieSearch.Application.UseCases.GetSingleRequestEntry;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.GetSingleRequestEntry
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/request-entries")]
    [ApiController]
    public class RequestEntryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RequestEntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<SingleRequestEntryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingleRequestEntryAsync(
            [Required] Guid id)
        {
            var queryResult = await _mediator.Send(new GetSingleRequestEntryQuery(id));
            return Output.For(queryResult);
        }
    }
}