﻿using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Domain.RequestFeatures;
using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetDepartmentsByResultsQueryHandler(IActualPerformanceResultRepository repository, IMapper mapper) : IRequestHandler<GetDepartmentsByResultsQuery, IEnumerable<DepartmentWithResultDto>>
{
	private readonly IActualPerformanceResultRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<DepartmentWithResultDto>> Handle(GetDepartmentsByResultsQuery request, CancellationToken cancellationToken)
    {
        var entitiesWithMetaData = await _repository.GetActualPerformanceResultsByDepartmentAsync(
           request.ActualPerformanceResultParameters,
           false
       );

        var entitiesDto = _mapper.Map<IEnumerable<DepartmentWithResultDto>>(entitiesWithMetaData);
        entitiesDto = entitiesDto.GroupBy(p => new { p.Year, p.Quarter, p.Name })
            .Select(g => new DepartmentWithResultDto
            {
                Year = g.Key.Year,
                Quarter = g.Key.Quarter,
                Name = g.Key.Name,
                Value = g.Sum(x => x.Value)
            })
            .OrderByDescending(x => x.Value);

        return entitiesDto;
    }
}
