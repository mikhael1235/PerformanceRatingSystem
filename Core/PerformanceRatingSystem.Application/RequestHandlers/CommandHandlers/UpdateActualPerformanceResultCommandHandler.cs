using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class UpdateActualPerformanceResultCommandHandler : IRequestHandler<UpdateActualPerformanceResultCommand, bool>
{
	private readonly IActualPerformanceResultRepository _repository;
	private readonly IMapper _mapper;

	public UpdateActualPerformanceResultCommandHandler(IActualPerformanceResultRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateActualPerformanceResultCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.ActualPerformanceResult.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.ActualPerformanceResult, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
