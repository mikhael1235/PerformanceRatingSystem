using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetEmployeePerformanceIndicatorByIdQuery(Guid Id) : IRequest<EmployeePerformanceIndicatorDto?>;
