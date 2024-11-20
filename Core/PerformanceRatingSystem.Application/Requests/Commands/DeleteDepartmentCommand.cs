using MediatR;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record DeleteDepartmentCommand(Guid Id) : IRequest<bool>;
