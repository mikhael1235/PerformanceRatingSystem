using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetEmployeePerformanceIndicatorsQueryHandler : IRequestHandler<GetEmployeePerformanceIndicatorsQuery, IEnumerable<EmployeePerformanceIndicatorDto>>
{
	private readonly IEmployeePerformanceIndicatorRepository _repository;
	private readonly IMapper _mapper;

	public GetEmployeePerformanceIndicatorsQueryHandler(IEmployeePerformanceIndicatorRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<EmployeePerformanceIndicatorDto>> Handle(GetEmployeePerformanceIndicatorsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<EmployeePerformanceIndicatorDto>>(await _repository.Get(trackChanges: false));
}
