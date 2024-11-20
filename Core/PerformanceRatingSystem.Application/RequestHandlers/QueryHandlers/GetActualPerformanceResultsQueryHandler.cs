using MediatR;
using AutoMapper;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Application.Requests.Queries;

namespace PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;

public class GetActualPerformanceResultsQueryHandler : IRequestHandler<GetActualPerformanceResultsQuery, IEnumerable<ActualPerformanceResultDto>>
{
	private readonly IActualPerformanceResultRepository _repository;
	private readonly IMapper _mapper;

	public GetActualPerformanceResultsQueryHandler(IActualPerformanceResultRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<ActualPerformanceResultDto>> Handle(GetActualPerformanceResultsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<ActualPerformanceResultDto>>(await _repository.Get(trackChanges: false));
}
