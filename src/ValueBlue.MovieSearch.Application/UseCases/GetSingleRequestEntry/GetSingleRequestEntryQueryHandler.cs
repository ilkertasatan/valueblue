using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Application.UseCases.GetSingleRequestEntry
{
    public class GetSingleRequestEntryQueryHandler :
        IRequestHandler<GetSingleRequestEntryQuery, IQueryResult>
    {
        private readonly IRepository<RequestEntry> _repository;

        public GetSingleRequestEntryQueryHandler(IRepository<RequestEntry> repository)
        {
            _repository = repository;
        }

        public async Task<IQueryResult> Handle(
            GetSingleRequestEntryQuery request,
            CancellationToken cancellationToken)
        {
            var requestEntry = await _repository.FindOneAsync(r => r.Id == request.Id, cancellationToken);
            if (!requestEntry.Exists())
                return new RequestEntryNotFoundResult();
            
            return new GetSingleRequestEntrySuccessResult(requestEntry);
        }
    }
}