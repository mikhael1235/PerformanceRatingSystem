using MediatR;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record DeleteAchievementCommand(Guid Id) : IRequest<bool>;
