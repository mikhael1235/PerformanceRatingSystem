using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Queries;

public record GetAchievementByIdQuery(Guid Id) : IRequest<AchievementDto?>;
