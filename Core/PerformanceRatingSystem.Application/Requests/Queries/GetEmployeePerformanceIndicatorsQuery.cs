using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetEmployeePerformanceIndicatorsQuery : IRequest<IEnumerable<EmployeePerformanceIndicatorDto>>;
