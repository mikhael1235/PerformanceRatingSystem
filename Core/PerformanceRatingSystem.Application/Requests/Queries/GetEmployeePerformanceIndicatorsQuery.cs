using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetEmployeePerformanceIndicatorsQuery(EmployeePerformanceIndicatorParameters EmployeePerformanceIndicatorParameters) :
    IRequest<PagedList<EmployeePerformanceIndicatorDto>>;
