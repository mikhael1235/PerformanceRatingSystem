using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand>
{
	private readonly IDepartmentRepository _repository;
	private readonly IMapper _mapper;

	public CreateDepartmentCommandHandler(IDepartmentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Department>(request.Department));
		await _repository.SaveChanges();
	}
}
