using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record UpdatePlannedPerformanceValueCommand(PlannedPerformanceValueForUpdateDto PlannedPerformanceValue) : IRequest<bool>;
