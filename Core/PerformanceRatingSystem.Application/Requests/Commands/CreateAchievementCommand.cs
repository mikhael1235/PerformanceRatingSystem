using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record CreateAchievementCommand(AchievementForCreationDto Achievement) : IRequest;
