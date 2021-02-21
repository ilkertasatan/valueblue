using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.Movies;
using ValueBlue.MovieSearch.Infrastructure.DataAccess.Entities;

namespace ValueBlue.MovieSearch.Application.UseCases.SearchMovie
{
    public class MovieSearchRequestedEventHandler :
        INotificationHandler<MovieSearchRequested>
    {
        private readonly IRepository<MovieSearchRequest> _repository;

        public MovieSearchRequestedEventHandler(
            IRepository<MovieSearchRequest> repository)
        {
            _repository = repository;
        }

        public Task Handle(MovieSearchRequested notification, CancellationToken cancellationToken)
        {
            var movieRequest = new MovieSearchRequestEntity(
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