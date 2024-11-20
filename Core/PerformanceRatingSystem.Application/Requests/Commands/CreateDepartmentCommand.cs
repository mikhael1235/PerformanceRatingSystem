using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record CreateDepartmentCommand(DepartmentForCreationDto Department) : IRequest;
