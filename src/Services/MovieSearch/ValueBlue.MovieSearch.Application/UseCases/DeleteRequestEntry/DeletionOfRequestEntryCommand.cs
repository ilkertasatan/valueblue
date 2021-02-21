using System;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;

namespace ValueBlue.MovieSearch.Application.UseCases.DeleteRequestEntry
{
    public sealed class DeletionOfRequestEntryCommand : IRequest<ICommandResult>
    {
        public DeletionOfRequestEntryCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}