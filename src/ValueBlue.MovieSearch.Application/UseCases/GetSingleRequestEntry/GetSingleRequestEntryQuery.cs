using System;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;

namespace ValueBlue.MovieSearch.Application.UseCases.GetSingleRequestEntry
{
    public sealed class GetSingleRequestEntryQuery :
        IRequest<IQueryResult>
    {
        public GetSingleRequestEntryQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}