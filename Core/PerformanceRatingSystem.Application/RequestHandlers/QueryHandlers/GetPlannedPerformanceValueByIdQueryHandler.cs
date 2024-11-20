using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetPlannedPerformanceValueByIdQueryHandler : IRequestHandler<GetPlannedPerformanceValueByIdQuery, PlannedPerformanceValueDto?>
{
	private readonly IPlannedPerformanceValueRepository _repository;
	private readonly IMapper _mapper;

	public GetPlannedPerformanceValueByIdQueryHandler(IPlannedPerformanceValueRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PlannedPerformanceValueDto?> Handle(GetPlannedPerformanceValueByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<PlannedPerformanceValueDto>(await _repository.GetById(request.Id, trackChanges: false));
}
