using System;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;

namespace ValueBlue.MovieSearch.Application.UseCases.GetSingleRequestEntry
{
    public sealed class SingleRequestEntryQuery :
        IRequest<IQueryResult>
    {
        public SingleRequestEntryQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}