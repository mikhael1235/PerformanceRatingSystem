using MediatR;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record DeletePlannedPerformanceValueCommand(Guid Id) : IRequest<bool>;
