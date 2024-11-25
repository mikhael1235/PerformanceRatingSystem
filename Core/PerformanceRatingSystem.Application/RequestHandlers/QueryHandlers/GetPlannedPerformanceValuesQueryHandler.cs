using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetPlannedPerformanceValuesQueryHandler : IRequestHandler<GetPlannedPerformanceValuesQuery, PagedList<PlannedPerformanceValueDto>>
{
	private readonly IPlannedPerformanceValueRepository _repository;
	private readonly IMapper _mapper;

	public GetPlannedPerformanceValuesQueryHandler(IPlannedPerformanceValueRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedList<PlannedPerformanceValueDto>> Handle(GetPlannedPerformanceValuesQuery request, CancellationToken cancellationToken)
	{
        var employeesWithMetaData = await _repository.Get(request.PlannedPerformanceValueParameters, trackChanges: false);

        var employeeDtos = _mapper.Map<IEnumerable<PlannedPerformanceValueDto>>(employeesWithMetaData);

        var employeesDtoWithMetaData = new PagedList<PlannedPerformanceValueDto>(
            employeeDtos.ToList(),
            employeesWithMetaData.MetaData.TotalCount,
            request.PlannedPerformanceValueParameters.PageNumber,
            request.PlannedPerformanceValueParameters.PageSize
        );

        return employeesDtoWithMetaData;
    }
}
