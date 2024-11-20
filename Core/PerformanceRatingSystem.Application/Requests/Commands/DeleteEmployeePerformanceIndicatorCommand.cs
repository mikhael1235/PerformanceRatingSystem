using MediatR;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record DeleteEmployeePerformanceIndicatorCommand(Guid Id) : IRequest<bool>;
