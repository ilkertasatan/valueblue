using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueBlue.MovieSearch.Api.UseCases.V1.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Application.UseCases.GetAllRequestEntries;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.GetAllRequestEntries
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

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SingleRequestEntryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequestEntriesAsync()
        {
            var queryResult = await _mediator.Send(new AllRequestEntriesQuery());
            return Output.For(queryResult);
        }
    }
}