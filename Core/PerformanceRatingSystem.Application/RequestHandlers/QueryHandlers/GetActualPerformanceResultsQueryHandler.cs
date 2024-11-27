using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetActualPerformanceResultsQueryHandler : IRequestHandler<GetActualPerformanceResultsQuery, PagedList<ActualPerformanceResultDto>>
{
	private readonly IActualPerformanceResultRepository _repository;
	private readonly IMapper _mapper;

	public GetActualPerformanceResultsQueryHandler(IActualPerformanceResultRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedList<ActualPerformanceResultDto>> Handle(GetActualPerformanceResultsQuery request, CancellationToken cancellationToken)
	{
        var employeesWithMetaData = await _repository.Get(request.ActualPerformanceResultParameters, false);

        var entitiesDto = _mapper.Map<IEnumerable<ActualPerformanceResultDto>>(employeesWithMetaData);

        var employeesDtoWithMetaData = new PagedList<ActualPerformanceResultDto>(
            entitiesDto.ToList(),
            employeesWithMetaData.MetaData.TotalCount,
            request.ActualPerformanceResultParameters.PageNumber,
            request.ActualPerformanceResultParameters.PageSize
        );

        return employeesDtoWithMetaData;
    }
		
}
