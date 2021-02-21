using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.Movies;
using ValueBlue.MovieSearch.Infrastructure.DataAccess.Entities;

namespace ValueBlue.MovieSearch.Application.UseCases.SearchMovie
{
    public class MovieSearchedEventHandler :
        INotificationHandler<MovieSearched>
    {
        private readonly IRepository<RequestEntry> _repository;

        public MovieSearchedEventHandler(
            IRepository<RequestEntry> repository)
        {
            _repository = repository;
        }

        public Task Handle(MovieSearched notification, CancellationToken cancellationToken)
        {
            var movieRequest = new RequestEntryEntity(
                notification.SearchToken,
                notification.ImdbId,
                notification.ProcessingTime,
                notification.Timestamp,
                notification.IpAddress);

            _repository.InsertOneAsync(movieRequest, cancellationToken);

            return Task.CompletedTask;
        }
    }
}