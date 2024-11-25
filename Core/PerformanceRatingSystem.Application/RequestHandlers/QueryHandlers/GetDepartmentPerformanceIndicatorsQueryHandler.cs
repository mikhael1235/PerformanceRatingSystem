using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetDepartmentPerformanceIndicatorsQueryHandler(IDepartmentPerformanceIndicatorRepository repository, IMapper mapper) : IRequestHandler<GetDepartmentPerformanceIndicatorsQuery, PagedList<DepartmentPerformanceIndicatorDto>>
{
	private readonly IDepartmentPerformanceIndicatorRepository _repository = repository;
	private readonly IMapper _mapper = mapper;

	public async Task<PagedList<DepartmentPerformanceIndicatorDto>> Handle(GetDepartmentPerformanceIndicatorsQuery request, CancellationToken cancellationToken)
	{
        var employeesWithMetaData = await _repository.Get(request.DepartmentPerformanceIndicatorParameters, trackChanges: false);

        var employeeDtos = _mapper.Map<IEnumerable<DepartmentPerformanceIndicatorDto>>(employeesWithMetaData);

        var employeesDtoWithMetaData = new PagedList<DepartmentPerformanceIndicatorDto>(
            employeeDtos.ToList(),
            employeesWithMetaData.MetaData.TotalCount,
            request.DepartmentPerformanceIndicatorParameters.PageNumber,
            request.DepartmentPerformanceIndicatorParameters.PageSize
        );

        return employeesDtoWithMetaData;
    }
}
