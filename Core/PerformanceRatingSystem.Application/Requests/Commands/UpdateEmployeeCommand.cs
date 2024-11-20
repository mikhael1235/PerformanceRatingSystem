using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record UpdateEmployeeCommand(EmployeeForUpdateDto Employee) : IRequest<bool>;
