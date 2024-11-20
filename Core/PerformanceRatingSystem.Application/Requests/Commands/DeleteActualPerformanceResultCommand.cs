using MediatR;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record DeleteActualPerformanceResultCommand(Guid Id) : IRequest<bool>;
