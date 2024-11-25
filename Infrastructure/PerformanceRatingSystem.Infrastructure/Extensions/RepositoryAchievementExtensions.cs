using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using PerformanceRatingSystem.Infrastructure.Extensions.Utility;
using PerformanceRatingSystem.Domain.Entities;
namespace PerformanceRatingSystem.Infrastructure.Extensions;

public static class RepositoryAchievementExtensions
{
    public static IQueryable<Achievement> SearchByDescription(this IQueryable<Achievement> achievements, string searchDescription)
    {
        if (string.IsNullOrWhiteSpace(searchDescription))
            return achievements;

        var lowerCaseTerm = searchDescription.Trim().ToLower();

        return achievements.Where(e => e.Description.ToLower().Contains(searchDescription));
    }

    public static IQueryable<Achievement> Sort(this IQueryable<Achievement> achievements, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return achievements.OrderBy(e => e.DateAchieved);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Achievement>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return achievements.OrderBy(e => e.DateAchieved);

        return achievements.OrderBy(orderQuery);
    }
}
