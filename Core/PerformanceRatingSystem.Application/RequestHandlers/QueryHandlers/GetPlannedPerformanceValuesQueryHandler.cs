using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetPlannedPerformanceValuesQueryHandler : IRequestHandler<GetPlannedPerformanceValuesQuery, IEnumerable<PlannedPerformanceValueDto>>
{
	private readonly IPlannedPerformanceValueRepository _repository;
	private readonly IMapper _mapper;

	public GetPlannedPerformanceValuesQueryHandler(IPlannedPerformanceValueRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<PlannedPerformanceValueDto>> Handle(GetPlannedPerformanceValuesQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<PlannedPerformanceValueDto>>(await _repository.Get(trackChanges: false));
}
