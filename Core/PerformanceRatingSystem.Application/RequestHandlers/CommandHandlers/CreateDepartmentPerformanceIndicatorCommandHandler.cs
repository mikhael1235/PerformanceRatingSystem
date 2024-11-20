using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class CreateDepartmentPerformanceIndicatorCommandHandler : IRequestHandler<CreateDepartmentPerformanceIndicatorCommand>
{
	private readonly IDepartmentPerformanceIndicatorRepository _repository;
	private readonly IMapper _mapper;

	public CreateDepartmentPerformanceIndicatorCommandHandler(IDepartmentPerformanceIndicatorRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateDepartmentPerformanceIndicatorCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<DepartmentPerformanceIndicator>(request.DepartmentPerformanceIndicator));
		await _repository.SaveChanges();
	}
}
