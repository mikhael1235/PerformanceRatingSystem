using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetAchievementsQueryHandler : IRequestHandler<GetAchievementsQuery, PagedList<AchievementDto>>
{
	private readonly IAchievementRepository _repository;
	private readonly IMapper _mapper;

	public GetAchievementsQueryHandler(IAchievementRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedList<AchievementDto>> Handle(GetAchievementsQuery request, CancellationToken cancellationToken)
	{
        var employeesWithMetaData = await _repository.Get(request.AchievementParameters, trackChanges: false);

        var employeeDtos = _mapper.Map<IEnumerable<AchievementDto>>(employeesWithMetaData);

        var employeesDtoWithMetaData = new PagedList<AchievementDto>(
            employeeDtos.ToList(),
            employeesWithMetaData.MetaData.TotalCount,
            request.AchievementParameters.PageNumber,
            request.AchievementParameters.PageSize
        );

        return employeesDtoWithMetaData;
    }
		
}
