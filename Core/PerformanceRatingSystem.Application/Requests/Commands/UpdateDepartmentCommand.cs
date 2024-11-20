using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record UpdateDepartmentCommand(DepartmentForUpdateDto Department) : IRequest<bool>;
