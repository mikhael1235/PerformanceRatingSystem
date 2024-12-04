using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using PerformanceRatingSystem.Infrastructure.Extensions.Utility;
using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Infrastructure.Extensions;

public static class RepositoryActualPerformanceResultExtensions
{
    public static IQueryable<ActualPerformanceResult> Search(this IQueryable<ActualPerformanceResult> plannedValues, string searchQuarter, string searchYear, Guid? searchDepartment)
    {
        if(searchDepartment == null)
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

        if (string.IsNullOrWhiteSpace(searchQuarter) && string.IsNullOrWhiteSpace(searchYear))
            return plannedValues.Where(x => x.Indicator.Employee.DepartmentId == searchDepartment);

        if (string.IsNullOrWhiteSpace(searchYear))
            return plannedValues.Where(x => x.Quarter == byte.Parse(searchQuarter) && x.Indicator.Employee.DepartmentId == searchDepartment);

        if (string.IsNullOrWhiteSpace(searchQuarter))
            return plannedValues.Where(x => x.Year == short.Parse(searchYear) && x.Indicator.Employee.DepartmentId == searchDepartment);

        return plannedValues.Where(e => 
            e.Quarter == byte.Parse(searchQuarter) && 
            e.Year == short.Parse(searchYear) && 
            e.Indicator.Employee.DepartmentId == searchDepartment);
    }

    public static IQueryable<ActualPerformanceResult> Sort(this IQueryable<ActualPerformanceResult> products, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return products.OrderBy(e => e.Value);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<ActualPerformanceResult>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return products.OrderBy(e => e.Value);

        return products.OrderBy(orderQuery);
    }
}
