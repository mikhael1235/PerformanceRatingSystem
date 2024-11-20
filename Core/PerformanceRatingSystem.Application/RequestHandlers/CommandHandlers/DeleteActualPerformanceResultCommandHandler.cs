using MediatR;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class DeleteActualPerformanceResultCommandHandler(IActualPerformanceResultRepository repository) : IRequestHandler<DeleteActualPerformanceResultCommand, bool>
{
	private readonly IActualPerformanceResultRepository _repository = repository;

	public async Task<bool> Handle(DeleteActualPerformanceResultCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Id, trackChanges: false);

        if (entity is null)
        {
            return false;
        }

        _repository.Delete(entity);
        await _repository.SaveChanges();

        return true;
	}
}
