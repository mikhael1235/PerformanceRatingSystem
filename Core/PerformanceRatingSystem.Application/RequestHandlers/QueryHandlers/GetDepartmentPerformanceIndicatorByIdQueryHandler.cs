using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetDepartmentPerformanceIndicatorByIdQueryHandler : IRequestHandler<GetDepartmentPerformanceIndicatorByIdQuery, DepartmentPerformanceIndicatorDto?>
{
	private readonly IDepartmentPerformanceIndicatorRepository _repository;
	private readonly IMapper _mapper;

	public GetDepartmentPerformanceIndicatorByIdQueryHandler(IDepartmentPerformanceIndicatorRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<DepartmentPerformanceIndicatorDto?> Handle(GetDepartmentPerformanceIndicatorByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<DepartmentPerformanceIndicatorDto>(await _repository.GetById(request.Id, trackChanges: false));
}
