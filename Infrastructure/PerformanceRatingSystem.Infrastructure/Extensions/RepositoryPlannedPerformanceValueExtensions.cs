using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using PerformanceRatingSystem.Infrastructure.Extensions.Utility;
using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Infrastructure.Extensions;

public static class RepositoryPlannedPerformanceValueExtensions
{
    public static IQueryable<PlannedPerformanceValue> Search(this IQueryable<PlannedPerformanceValue> plannedValues, string searchQuarter, string searchYear)
    {
        if (string.IsNullOrWhiteSpace(searchQuarter) && string.IsNullOrWhiteSpace(searchYear))
            return plannedValues;

        if (string.IsNullOrWhiteSpace(searchYear))
            return plannedValues.Where(x => x.Quarter == byte.Parse(searchQuarter));

        if (string.IsNullOrWhiteSpace(searchQuarter))
            return plannedValues.Where(x => x.Year == short.Parse(searchYear));

        return plannedValues.Where(e =>
            e.Quarter == byte.Parse(searchQuarter) &&
            e.Year == short.Parse(searchYear));
    }

    public static IQueryable<PlannedPerformanceValue> Sort(this IQueryable<PlannedPerformanceValue> plannedValues, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return plannedValues.OrderBy(e => e.Value);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<PlannedPerformanceValue>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return plannedValues.OrderBy(e => e.Value);

        return plannedValues.OrderBy(orderQuery);
    }
}
