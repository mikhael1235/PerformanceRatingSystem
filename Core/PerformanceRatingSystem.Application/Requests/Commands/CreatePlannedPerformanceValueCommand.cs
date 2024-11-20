using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record CreatePlannedPerformanceValueCommand(PlannedPerformanceValueForCreationDto PlannedPerformanceValue) : IRequest;
