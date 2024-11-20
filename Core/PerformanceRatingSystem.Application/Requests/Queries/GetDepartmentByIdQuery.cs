using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetDepartmentByIdQuery(Guid Id) : IRequest<DepartmentDto?>;
