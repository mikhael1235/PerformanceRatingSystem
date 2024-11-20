using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Application.RequestHandlers.CommandHandlers;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, bool>
{
	private readonly IDepartmentRepository _repository;
	private readonly IMapper _mapper;

	public UpdateDepartmentCommandHandler(IDepartmentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Department.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Department, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
