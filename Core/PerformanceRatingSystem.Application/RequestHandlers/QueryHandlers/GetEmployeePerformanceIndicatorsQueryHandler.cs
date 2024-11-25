using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetEmployeePerformanceIndicatorsQueryHandler : IRequestHandler<GetEmployeePerformanceIndicatorsQuery, PagedList<EmployeePerformanceIndicatorDto>>
{
	private readonly IEmployeePerformanceIndicatorRepository _repository;
	private readonly IMapper _mapper;

	public GetEmployeePerformanceIndicatorsQueryHandler(IEmployeePerformanceIndicatorRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedList<EmployeePerformanceIndicatorDto>> Handle(GetEmployeePerformanceIndicatorsQuery request, CancellationToken cancellationToken)
	{
        var employeesWithMetaData = await _repository.Get(request.EmployeePerformanceIndicatorParameters, trackChanges: false);

        var employeeDtos = _mapper.Map<IEnumerable<EmployeePerformanceIndicatorDto>>(employeesWithMetaData);

        var employeesDtoWithMetaData = new PagedList<EmployeePerformanceIndicatorDto>(
            employeeDtos.ToList(),
            employeesWithMetaData.MetaData.TotalCount,
            request.EmployeePerformanceIndicatorParameters.PageNumber,
            request.EmployeePerformanceIndicatorParameters.PageSize
        );

        return employeesDtoWithMetaData;
    }
	
}
