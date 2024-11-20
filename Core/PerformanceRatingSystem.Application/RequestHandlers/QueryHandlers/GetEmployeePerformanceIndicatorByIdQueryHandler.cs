using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetEmployeePerformanceIndicatorByIdQueryHandler : IRequestHandler<GetEmployeePerformanceIndicatorByIdQuery, EmployeePerformanceIndicatorDto?>
{
	private readonly IEmployeePerformanceIndicatorRepository _repository;
	private readonly IMapper _mapper;

	public GetEmployeePerformanceIndicatorByIdQueryHandler(IEmployeePerformanceIndicatorRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<EmployeePerformanceIndicatorDto?> Handle(GetEmployeePerformanceIndicatorByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<EmployeePerformanceIndicatorDto>(await _repository.GetById(request.Id, trackChanges: false));
}
