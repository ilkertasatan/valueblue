using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;

namespace ValueBlue.MovieSearch.Application.UseCases.GetAllRequestEntries
{
    public sealed class AllRequestEntriesQuery :
        IRequest<IQueryResult>
    {
    }
}