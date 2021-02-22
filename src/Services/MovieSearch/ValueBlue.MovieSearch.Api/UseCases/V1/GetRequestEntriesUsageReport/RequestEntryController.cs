using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueBlue.MovieSearch.Api.UseCases.V1.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesUsageReport;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.GetRequestEntriesUsageReport
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

        [HttpGet("usage-report")]
        [ProducesResponseType(typeof(IEnumerable<SingleRequestEntryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRequestEntriesUsageReportAsync(
            [FromQuery]GetRequestEntriesUsageReportRequest request)
        {
            var queryResult = await _mediator.Send(new GetRequestEntriesUsageReportQuery(request.Timestamp));
            return Output.For(queryResult);
        }
    }
}