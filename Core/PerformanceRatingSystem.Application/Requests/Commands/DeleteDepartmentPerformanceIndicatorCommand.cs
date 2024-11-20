using MediatR;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record DeleteDepartmentPerformanceIndicatorCommand(Guid Id) : IRequest<bool>;
