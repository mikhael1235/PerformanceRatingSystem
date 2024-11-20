using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand>
{
	private readonly IEmployeeRepository _repository;
	private readonly IMapper _mapper;

	public CreateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Employee>(request.Employee));
		await _repository.SaveChanges();
	}
}
