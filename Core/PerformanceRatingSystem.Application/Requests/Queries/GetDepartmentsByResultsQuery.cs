using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetDepartmentsByResultsQuery(ActualPerformanceResultParameters ActualPerformanceResultParameters) 
    : IRequest<IEnumerable<DepartmentWithResultDto>>;
