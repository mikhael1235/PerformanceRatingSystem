using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetDepartmentsQuery(DepartmentParameters DepartmentParameters) :
    IRequest<PagedList<DepartmentDto>>;