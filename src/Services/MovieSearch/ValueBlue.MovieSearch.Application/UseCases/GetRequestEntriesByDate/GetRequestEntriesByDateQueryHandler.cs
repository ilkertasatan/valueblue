using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesByDate
{
    public class GetRequestEntriesByDateQueryHandler : 
        IRequestHandler<GetRequestEntriesByDateQuery, IQueryResult>
    {
        private readonly IRepository<RequestEntry> _repository;

        public GetRequestEntriesByDateQueryHandler(
            IRepository<RequestEntry> repository)
        {
            _repository = repository;
        }

        public async Task<IQueryResult> Handle(
            GetRequestEntriesByDateQuery request,
            CancellationToken cancellationToken)
        {
            var requestEntries = await _repository.FindManyAsync(r =>
                    r.Timestamp >= request.From &&
                    r.Timestamp <= request.End
                , cancellationToken);
            return new AllRequestEntriesSuccessResult(requestEntries);
        }
    }
}