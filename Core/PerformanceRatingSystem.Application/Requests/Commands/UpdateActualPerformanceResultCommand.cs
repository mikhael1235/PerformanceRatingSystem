using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record UpdateActualPerformanceResultCommand(ActualPerformanceResultForUpdateDto ActualPerformanceResult) : IRequest<bool>;
