using System;

namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public sealed class ImdbInfo :
        ValueObject<ImdbInfo>
    {
        private ImdbInfo(
            string imdbId,
            string votes,
            string rating)
        {
            ImdbId = imdbId;
            Votes = votes;
            Rating = rating;
        }

        public string ImdbId { get; }
        public string Votes { get; }
        public string Rating { get; }

        public static ImdbInfo New(
            string imdbId,
            string votes,
            string rating)
        {
            return new ImdbInfo(imdbId, votes, rating);
        }

        protected override bool EqualsCore(ImdbInfo other)
        {
            return ImdbId == other.ImdbId &&
                   Votes == other.Votes &&
                   Rating == other.Rating;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(GetHashCode(), ImdbId, Votes, Rating);
        }
    }
}