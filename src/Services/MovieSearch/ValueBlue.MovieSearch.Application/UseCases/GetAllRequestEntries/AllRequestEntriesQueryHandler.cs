using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Application.UseCases.GetAllRequestEntries
{
    public class AllRequestEntriesQueryHandler : 
        IRequestHandler<AllRequestEntriesQuery, IQueryResult>
    {
        private readonly IRepository<RequestEntry> _repository;

        public AllRequestEntriesQueryHandler(IRepository<RequestEntry> repository)
        {
            _repository = repository;
        }

        public async Task<IQueryResult> Handle(AllRequestEntriesQuery request, CancellationToken cancellationToken)
        {
            var requestEntries = await _repository.FindManyAsync(_ => true, cancellationToken);
            return new AllRequestEntriesSuccessResult(requestEntries);
        }
    }
}