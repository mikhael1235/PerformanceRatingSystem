using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record UpdateEmployeePerformanceIndicatorCommand(EmployeePerformanceIndicatorForUpdateDto EmployeePerformanceIndicator) : IRequest<bool>;
