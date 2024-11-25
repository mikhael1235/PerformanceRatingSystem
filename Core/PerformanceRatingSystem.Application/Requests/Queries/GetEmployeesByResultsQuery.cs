using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetEmployeesByResultsQuery(ActualPerformanceResultParameters ActualPerformanceResultParameters) 
    : IRequest<PagedList<EmployeeWithResultDto>>;
