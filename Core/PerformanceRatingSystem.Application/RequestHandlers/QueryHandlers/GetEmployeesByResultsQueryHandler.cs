using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Domain.RequestFeatures;
using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetEmployeesByResultsQueryHandler(IActualPerformanceResultRepository repository, IMapper mapper) : IRequestHandler<GetEmployeesByResultsQuery, IEnumerable<EmployeeWithResultDto>>
{
	private readonly IActualPerformanceResultRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<EmployeeWithResultDto>> Handle(GetEmployeesByResultsQuery request, CancellationToken cancellationToken)
    {
        var entitiesWithMetaData = await _repository.GetActualPerformanceResultsByDepartmentAsync(
           request.ActualPerformanceResultParameters,
           false
       );

        var entitiesDto = _mapper.Map<IEnumerable<EmployeeWithResultDto>>(entitiesWithMetaData);
        entitiesDto = entitiesDto.GroupBy(p => new { p.Year, p.Quarter, p.FullName })
            .Select(g => new EmployeeWithResultDto
            {
                Year = g.Key.Year,
                Quarter = g.Key.Quarter,
                FullName = g.Key.FullName,
                Value = g.Sum(x => x.Value)
            })
            .OrderByDescending(x => x.Value);

        return entitiesDto;
    }
}
