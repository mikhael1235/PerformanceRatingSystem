using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetActualPerformanceResultByIdQueryHandler : IRequestHandler<GetActualPerformanceResultByIdQuery, ActualPerformanceResultDto?>
{
	private readonly IActualPerformanceResultRepository _repository;
	private readonly IMapper _mapper;

	public GetActualPerformanceResultByIdQueryHandler(IActualPerformanceResultRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<ActualPerformanceResultDto?> Handle(GetActualPerformanceResultByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<ActualPerformanceResultDto>(await _repository.GetById(request.Id, trackChanges: false));
}
