using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetPlannedPerformanceValueByIdQuery(Guid Id) : IRequest<PlannedPerformanceValueDto?>;
