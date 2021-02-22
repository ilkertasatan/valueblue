using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.GetRequestEntriesByDate
{
    public sealed class GetRequestEntriesByDateRequest
    {
        [FromQuery(Name = "from")] 
        [Required]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        
        [FromQuery(Name = "end")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
    }
}