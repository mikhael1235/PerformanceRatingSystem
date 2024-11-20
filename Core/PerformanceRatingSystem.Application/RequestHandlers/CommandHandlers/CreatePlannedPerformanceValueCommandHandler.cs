using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class CreatePlannedPerformanceValueCommandHandler : IRequestHandler<CreatePlannedPerformanceValueCommand>
{
	private readonly IPlannedPerformanceValueRepository _repository;
	private readonly IMapper _mapper;

	public CreatePlannedPerformanceValueCommandHandler(IPlannedPerformanceValueRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreatePlannedPerformanceValueCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<PlannedPerformanceValue>(request.PlannedPerformanceValue));
		await _repository.SaveChanges();
	}
}
