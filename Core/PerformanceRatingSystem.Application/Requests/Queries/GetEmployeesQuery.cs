using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>;
