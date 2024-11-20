using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class CreateEmployeePerformanceIndicatorCommandHandler : IRequestHandler<CreateEmployeePerformanceIndicatorCommand>
{
	private readonly IEmployeePerformanceIndicatorRepository _repository;
	private readonly IMapper _mapper;

	public CreateEmployeePerformanceIndicatorCommandHandler(IEmployeePerformanceIndicatorRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateEmployeePerformanceIndicatorCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<EmployeePerformanceIndicator>(request.EmployeePerformanceIndicator));
		await _repository.SaveChanges();
	}
}
