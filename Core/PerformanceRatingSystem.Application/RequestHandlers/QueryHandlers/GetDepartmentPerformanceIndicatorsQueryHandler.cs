using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetDepartmentPerformanceIndicatorsQueryHandler(IDepartmentPerformanceIndicatorRepository repository, IMapper mapper) : IRequestHandler<GetDepartmentPerformanceIndicatorsQuery, IEnumerable<DepartmentPerformanceIndicatorDto>>
{
	private readonly IDepartmentPerformanceIndicatorRepository _repository = repository;
	private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<DepartmentPerformanceIndicatorDto>> Handle(GetDepartmentPerformanceIndicatorsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<DepartmentPerformanceIndicatorDto>>(await _repository.Get(trackChanges: false));
}
