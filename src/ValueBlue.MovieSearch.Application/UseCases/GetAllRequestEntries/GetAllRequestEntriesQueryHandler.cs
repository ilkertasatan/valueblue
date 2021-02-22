using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Application.UseCases.GetAllRequestEntries
{
    public class GetAllRequestEntriesQueryHandler : 
        IRequestHandler<GetAllRequestEntriesQuery, IQueryResult>
    {
        private readonly IRepository<RequestEntry> _repository;

        public GetAllRequestEntriesQueryHandler(
            IRepository<RequestEntry> repository)
        {
            _repository = repository;
        }

        public async Task<IQueryResult> Handle(
            GetAllRequestEntriesQuery request,
            CancellationToken cancellationToken)
        {
            var requestEntries = await _repository.FindManyAsync(_ => true, cancellationToken);
            return new GetRequestEntriesSuccessResult(requestEntries);
        }
    }
}