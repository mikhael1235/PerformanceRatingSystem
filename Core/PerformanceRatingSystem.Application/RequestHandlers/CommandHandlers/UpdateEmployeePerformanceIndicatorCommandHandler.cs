using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class UpdateEmployeePerformanceIndicatorCommandHandler : IRequestHandler<UpdateEmployeePerformanceIndicatorCommand, bool>
{
	private readonly IEmployeePerformanceIndicatorRepository _repository;
	private readonly IMapper _mapper;

	public UpdateEmployeePerformanceIndicatorCommandHandler(IEmployeePerformanceIndicatorRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateEmployeePerformanceIndicatorCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.EmployeePerformanceIndicator.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.EmployeePerformanceIndicator, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
