﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Application.UseCases.DeleteRequestEntry;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.DeleteRequestEntry
{
    public static class Output
    {
        public static IActionResult For(ICommandResult output) =>
            output switch
            {
                DeleteRequestEntrySuccessResult _ => NoContent(),
                RequestEntryNotFoundResult _ => NotFound(),
                _ => InternalServerError()
            };

        private static IActionResult NoContent()
        {
            return new NoContentResult();
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