using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class CreateAchievementCommandHandler : IRequestHandler<CreateAchievementCommand>
{
	private readonly IAchievementRepository _repository;
	private readonly IMapper _mapper;

	public CreateAchievementCommandHandler(IAchievementRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateAchievementCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Achievement>(request.Achievement));
		await _repository.SaveChanges();
	}
}
