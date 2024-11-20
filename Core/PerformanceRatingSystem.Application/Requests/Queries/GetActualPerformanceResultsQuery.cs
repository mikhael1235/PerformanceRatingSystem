using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetActualPerformanceResultsQuery : IRequest<IEnumerable<ActualPerformanceResultDto>>;
