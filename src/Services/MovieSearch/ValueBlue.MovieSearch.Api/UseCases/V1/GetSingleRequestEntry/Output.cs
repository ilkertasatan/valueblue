using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Application.UseCases.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.GetSingleRequestEntry
{
    public static class Output
    {
        public static IActionResult For(IQueryResult output) =>
            output switch
            {
                SingleRequestEntrySuccessResult result => Ok(result.RequestEntry),
                RequestEntryNotFoundResult _ => NotFound(),
                _ => InternalServerError()
            };

        private static IActionResult Ok(RequestEntry requestEntry)
        {
            return new OkObjectResult(new SingleRequestEntryResponse
            {
                Id = requestEntry.Id.ToString(),
                SearchToken = requestEntry.SearchToken,
                ImDbId = requestEntry.ImdbId,
                ProcessingTimeMs = requestEntry.ProcessingTime,
                Timestamp = requestEntry.Timestamp,
                IpAddress = requestEntry.IpAddress
            });
        }

        private static IActionResult NotFound()
        {
            return new NotFoundResult();
        }

        private static IActionResult InternalServerError()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}