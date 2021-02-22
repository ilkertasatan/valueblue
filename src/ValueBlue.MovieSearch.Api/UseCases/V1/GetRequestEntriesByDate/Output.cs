using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueBlue.MovieSearch.Api.UseCases.V1.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.GetRequestEntriesByDate
{
    public static class Output
    {
        public static IActionResult For(IQueryResult output) =>
            output switch
            {
                GetRequestEntriesSuccessResult result => Ok(result.RequestEntries),
                _ => InternalServerError()
            };

        private static IActionResult Ok(IEnumerable<RequestEntry> requestEntries)
        {
            return new OkObjectResult(requestEntries
                .Select(requestEntry => new SingleRequestEntryResponse
                {
                    Id = requestEntry.Id.ToString(),
                    SearchToken = requestEntry.SearchToken,
                    ImDbId = requestEntry.ImdbId,
                    ProcessingTimeMs = requestEntry.ProcessingTime,
                    Timestamp = requestEntry.Timestamp,
                    IpAddress = requestEntry.IpAddress
                })
                .ToList());
        }
        
        private static IActionResult InternalServerError()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}