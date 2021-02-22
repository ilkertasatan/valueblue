using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Application.UseCases.GetSingleRequestEntry
{
    public class GetSingleRequestEntrySuccessResult :
        IQueryResult
    {
        public GetSingleRequestEntrySuccessResult(RequestEntry requestEntry)
        {
            RequestEntry = requestEntry;
        }

        public RequestEntry RequestEntry { get; }
    }
}