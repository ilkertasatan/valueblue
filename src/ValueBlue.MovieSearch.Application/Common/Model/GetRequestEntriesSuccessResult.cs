using System.Collections.Generic;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Application.Common.Model
{
    public class GetRequestEntriesSuccessResult :
        IQueryResult
    {
        public GetRequestEntriesSuccessResult(
            IEnumerable<RequestEntry> requestEntries)
        {
            RequestEntries = requestEntries;
        }

        public IEnumerable<RequestEntry> RequestEntries { get; }
    }
}