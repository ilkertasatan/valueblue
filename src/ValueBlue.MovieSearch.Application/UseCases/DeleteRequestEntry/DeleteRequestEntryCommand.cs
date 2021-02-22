using System;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;

namespace ValueBlue.MovieSearch.Application.UseCases.DeleteRequestEntry
{
    public sealed class DeleteRequestEntryCommand : IRequest<ICommandResult>
    {
        public DeleteRequestEntryCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}