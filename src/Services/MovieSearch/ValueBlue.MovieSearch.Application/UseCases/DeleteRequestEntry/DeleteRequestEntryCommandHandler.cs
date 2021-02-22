using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Application.UseCases.DeleteRequestEntry
{
    public class DeleteRequestEntryCommandHandler :
        IRequestHandler<DeleteRequestEntryCommand, ICommandResult>
    {
        private readonly IRepository<RequestEntry> _repository;

        public DeleteRequestEntryCommandHandler(
            IRepository<RequestEntry> repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> Handle(
            DeleteRequestEntryCommand request,
            CancellationToken cancellationToken)
        {
            var requestEntry = await _repository.FindOneAsync(r => r.Id == request.Id, cancellationToken);
            if (!requestEntry.Exists())
                return new RequestEntryNotFoundResult();
            
            await _repository.DeleteOneAsync(x => x.Id == request.Id, cancellationToken);

            return new DeleteRequestEntrySuccessResult();
        }
    }
}