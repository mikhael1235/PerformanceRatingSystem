using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class UpdateDepartmentPerformanceIndicatorCommandHandler : IRequestHandler<UpdateDepartmentPerformanceIndicatorCommand, bool>
{
	private readonly IDepartmentPerformanceIndicatorRepository _repository;
	private readonly IMapper _mapper;

	public UpdateDepartmentPerformanceIndicatorCommandHandler(IDepartmentPerformanceIndicatorRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateDepartmentPerformanceIndicatorCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.DepartmentPerformanceIndicator.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.DepartmentPerformanceIndicator, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
