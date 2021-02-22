using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesUsageReport
{
    public class GetRequestEntriesUsageReportQueryHandler :
        IRequestHandler<GetRequestEntriesUsageReportQuery, IQueryResult>
    {
        private readonly IUsageReportRepository _repository;

        public GetRequestEntriesUsageReportQueryHandler(
            IUsageReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<IQueryResult> Handle(
            GetRequestEntriesUsageReportQuery request,
            CancellationToken cancellationToken)
        {
            var usages = await _repository.GroupByTimestampAsync(request.Timestamp, cancellationToken);
            return new GetRequestEntriesUsageReportSuccessResult(usages);
        }
    }
}