using MediatR;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class DeleteEmployeePerformanceIndicatorCommandHandler(IEmployeePerformanceIndicatorRepository repository) : IRequestHandler<DeleteEmployeePerformanceIndicatorCommand, bool>
{
	private readonly IEmployeePerformanceIndicatorRepository _repository = repository;

	public async Task<bool> Handle(DeleteEmployeePerformanceIndicatorCommand request, CancellationToken cancellationToken)
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
