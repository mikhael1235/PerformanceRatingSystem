using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetActualPerformanceResultByIdQuery(Guid Id) : IRequest<ActualPerformanceResultDto?>;
