using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetDepartmentsQueryHandler(IDepartmentRepository repository, IMapper mapper) : IRequestHandler<GetDepartmentsQuery, PagedList<DepartmentDto>>
{
	private readonly IDepartmentRepository _repository = repository;
	private readonly IMapper _mapper = mapper;

    public async Task<PagedList<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departmentsWithMetaData = await _repository.Get(request.DepartmentParameters, trackChanges: false);

        var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departmentsWithMetaData);

        var departmentsDtoWithMetaData = new PagedList<DepartmentDto>(
            departmentDtos.ToList(),
            departmentsWithMetaData.MetaData.TotalCount,
            request.DepartmentParameters.PageNumber,
            request.DepartmentParameters.PageSize
        );

        return departmentsDtoWithMetaData;
    }
}
