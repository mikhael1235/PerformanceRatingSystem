using MediatR;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class DeleteDepartmentPerformanceIndicatorCommandHandler(IDepartmentPerformanceIndicatorRepository repository) : IRequestHandler<DeleteDepartmentPerformanceIndicatorCommand, bool>
{
	private readonly IDepartmentPerformanceIndicatorRepository _repository = repository;

	public async Task<bool> Handle(DeleteDepartmentPerformanceIndicatorCommand request, CancellationToken cancellationToken)
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
