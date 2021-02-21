using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ValueBlue.MovieSearch.Infrastructure.DataAccess.Entities
{
    public sealed class RequestEntry :
        Domain.RequestEntries.RequestEntry
    {
        public RequestEntry(
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


        public override object Id { get; protected set; }

        [BsonElement("search_token")] 
        public override string SearchToken { get; protected set; }

        [BsonElement("imdbID")] 
        public override string ImdbId { get; protected set; }

        [BsonElement("processing_time_ms")]
        public override long ProcessingTime { get; protected set; }

        [BsonElement("timestamp")]
        public override DateTime Timestamp { get; protected set; }

        [BsonElement("ip_address")]
        public override string IpAddress { get; protected set; }
    }
}