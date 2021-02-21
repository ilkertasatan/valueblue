using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ValueBlue.MovieSearch.Domain.Movies;

namespace ValueBlue.MovieSearch.Infrastructure.DataAccess.Entities
{
    public sealed class MovieSearchRequestEntity :
        MovieSearchRequest
    {
        private MovieSearchRequestEntity()
        {
        }
        
        public MovieSearchRequestEntity(
            string searchToken,
            string imdbId,
            long processingTime,
            DateTime timestamp,
            string ipAddress)
        {
            Id = ObjectId.GenerateNewId();
            SearchToken = searchToken;
            ImdbId = imdbId;
            ProcessingTime = processingTime;
            Timestamp = timestamp;
            IpAddress = ipAddress;
        }
        
        public override object Id { get; }

        [BsonElement("search_token")]
        public override string SearchToken { get; }
        
        [BsonElement("imdbID")]
        public override string ImdbId { get; }
        
        [BsonElement("processing_time_ms")]
        public override long ProcessingTime { get; }
        
        [BsonElement("timestamp")]
        public override DateTime Timestamp { get; }
        
        [BsonElement("ip_address")]
        public override string IpAddress { get; }
    }
}