using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Application.UseCases.GetSingleRequestEntry
{
    public class SingleRequestEntryQueryHandler :
        IRequestHandler<SingleRequestEntryQuery, IQueryResult>
    {
        private readonly IRepository<RequestEntry> _repository;

        public SingleRequestEntryQueryHandler(IRepository<RequestEntry> repository)
        {
            _repository = repository;
        }

        public async Task<IQueryResult> Handle(SingleRequestEntryQuery request, CancellationToken cancellationToken)
        {
            var requestEntry = await _repository.FindOneAsync(r => r.Id == request.Id, cancellationToken);
            if (!requestEntry.Exists())
                return new RequestEntryNotFoundResult();
            
            return new SingleRequestEntrySuccessResult(requestEntry);
        }
    }
}