using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, IEnumerable<DepartmentDto>>
{
	private readonly IDepartmentRepository _repository;
	private readonly IMapper _mapper;

	public GetDepartmentsQueryHandler(IDepartmentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<DepartmentDto>>(await _repository.Get(trackChanges: false));
}
