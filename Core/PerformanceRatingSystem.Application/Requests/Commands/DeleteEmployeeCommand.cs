using MediatR;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record DeleteEmployeeCommand(Guid Id) : IRequest<bool>;
