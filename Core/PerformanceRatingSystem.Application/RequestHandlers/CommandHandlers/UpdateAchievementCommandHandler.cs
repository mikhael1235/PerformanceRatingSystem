using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class UpdateAchievementCommandHandler : IRequestHandler<UpdateAchievementCommand, bool>
{
	private readonly IAchievementRepository _repository;
	private readonly IMapper _mapper;

	public UpdateAchievementCommandHandler(IAchievementRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateAchievementCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Achievement.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Achievement, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
