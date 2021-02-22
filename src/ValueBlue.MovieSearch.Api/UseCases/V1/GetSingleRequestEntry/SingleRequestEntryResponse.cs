using System;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.GetSingleRequestEntry
{
    public sealed class SingleRequestEntryResponse
    {
        public string Id { get; set; }
        public string SearchToken { get; set; }
        public string ImDbId { get; set; }
        public long ProcessingTimeMs { get; set; }
        public DateTime Timestamp { get; set; }
        public string IpAddress { get; set; }
    }
}