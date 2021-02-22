using System;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;

namespace ValueBlue.MovieSearch.Application.UseCases.GetSingleRequestEntry
{
    public sealed class GetSingleRequestEntryQuery :
        IRequest<IQueryResult>
    {
        public GetSingleRequestEntryQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}