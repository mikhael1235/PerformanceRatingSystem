using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetPlannedPerformanceValuesQuery : IRequest<IEnumerable<PlannedPerformanceValueDto>>;
