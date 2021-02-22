using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueBlue.MovieSearch.Api.UseCases.V1.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesByDate;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.GetRequestEntriesByDate
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

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<SingleRequestEntryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRequestEntriesByDateAsync([FromQuery]GetRequestEntriesByDateRequest request)
        {
            var queryResult = await _mediator.Send(new GetRequestEntriesByDateQuery(request.From, request.End));
            return Output.For(queryResult);
        }
    }
}