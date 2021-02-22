using System;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;

namespace ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesByDate
{
    public class GetRequestEntriesByDateQuery :
        IRequest<IQueryResult>
    {
        public GetRequestEntriesByDateQuery(
            DateTime from,
            DateTime end)
        {
            From = @from;
            End = end;
        }
        
        public DateTime From { get; }
        public DateTime End { get; }
    }
}