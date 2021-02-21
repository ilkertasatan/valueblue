using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Application.UseCases.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Application.UseCases.DeleteRequestEntry
{
    public class DeletionOfRequestEntryCommandHandler :
        IRequestHandler<DeletionOfRequestEntryCommand, ICommandResult>
    {
        private readonly IRepository<RequestEntry> _repository;

        public DeletionOfRequestEntryCommandHandler(IRepository<RequestEntry> repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> Handle(
            DeletionOfRequestEntryCommand ofRequest,
            CancellationToken cancellationToken)
        {
            var requestEntry = await _repository.FindOneAsync(r => r.Id == ofRequest.Id, cancellationToken);
            if (!requestEntry.Exists())
                return new RequestEntryNotFoundResult();
            
            await _repository.DeleteOneAsync(x => x.Id == ofRequest.Id, cancellationToken);

            return new DeletionOfRequestEntrySuccessResult();
        }
    }
}