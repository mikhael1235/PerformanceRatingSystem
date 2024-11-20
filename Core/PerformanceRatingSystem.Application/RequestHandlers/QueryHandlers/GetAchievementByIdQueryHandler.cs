using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetAchievementByIdQueryHandler : IRequestHandler<GetAchievementByIdQuery, AchievementDto?>
{
	private readonly IAchievementRepository _repository;
	private readonly IMapper _mapper;

	public GetAchievementByIdQueryHandler(IAchievementRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<AchievementDto?> Handle(GetAchievementByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<AchievementDto>(await _repository.GetById(request.Id, trackChanges: false));
}
