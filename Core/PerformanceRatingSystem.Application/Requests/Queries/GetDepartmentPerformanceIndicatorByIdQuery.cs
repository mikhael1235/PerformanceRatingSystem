using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetDepartmentPerformanceIndicatorByIdQuery(Guid Id) : IRequest<DepartmentPerformanceIndicatorDto?>;
