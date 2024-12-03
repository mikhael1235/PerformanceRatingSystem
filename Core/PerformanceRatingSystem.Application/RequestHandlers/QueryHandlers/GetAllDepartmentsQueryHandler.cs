using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetAllDepartmentsQueryHandler(IDepartmentRepository repository, IMapper mapper) : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentDto>>
{
	private readonly IDepartmentRepository _repository = repository;
	private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _repository.GetAll(trackChanges: false);

        var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);

        return departmentDtos;
    }
}
