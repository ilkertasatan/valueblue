using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueBlue.MovieSearch.Application.UseCases.DeleteRequestEntry;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.DeleteRequestEntry
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRequestEntryAsync([Required] string id)
        {
            var queryResult = await _mediator.Send(new DeleteRequestEntryCommand(id));
            return Output.For(queryResult);
        }
    }
}