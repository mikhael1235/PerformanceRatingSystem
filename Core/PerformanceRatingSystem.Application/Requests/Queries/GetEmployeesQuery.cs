using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetEmployeesQuery(EmployeeParameters EmployeeParameters) :
    IRequest<PagedList<EmployeeDto>>;
