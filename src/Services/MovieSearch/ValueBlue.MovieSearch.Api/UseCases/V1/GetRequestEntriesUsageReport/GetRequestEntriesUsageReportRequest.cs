using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.GetRequestEntriesUsageReport
{
    public sealed class GetRequestEntriesUsageReportRequest
    {
        [FromQuery(Name = "timestamp")] 
        [Required]
        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }
    }
}