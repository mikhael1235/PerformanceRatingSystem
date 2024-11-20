using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class CreateActualPerformanceResultCommandHandler : IRequestHandler<CreateActualPerformanceResultCommand>
{
	private readonly IActualPerformanceResultRepository _repository;
	private readonly IMapper _mapper;

	public CreateActualPerformanceResultCommandHandler(IActualPerformanceResultRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateActualPerformanceResultCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<ActualPerformanceResult>(request.ActualPerformanceResult));
		await _repository.SaveChanges();
	}
}
