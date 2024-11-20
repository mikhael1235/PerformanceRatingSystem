using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class UpdatePlannedPerformanceValueCommandHandler : IRequestHandler<UpdatePlannedPerformanceValueCommand, bool>
{
	private readonly IPlannedPerformanceValueRepository _repository;
	private readonly IMapper _mapper;

	public UpdatePlannedPerformanceValueCommandHandler(IPlannedPerformanceValueRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdatePlannedPerformanceValueCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.PlannedPerformanceValue.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.PlannedPerformanceValue, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
