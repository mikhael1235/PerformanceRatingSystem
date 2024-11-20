using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record UpdateAchievementCommand(AchievementForUpdateDto Achievement) : IRequest<bool>;
