using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using PerformanceRatingSystem.Infrastructure.Extensions.Utility;
using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Infrastructure.Extensions;

public static class RepositoryActualPerformanceResultExtensions
{
    public static IQueryable<ActualPerformanceResult> Search(this IQueryable<ActualPerformanceResult> products, string searchQuarter, string searchYear, Guid? searchDepartment)
    {
        if (string.IsNullOrWhiteSpace(searchQuarter) || string.IsNullOrWhiteSpace(searchYear))
            return products.Where(e => e.Year == 2012 && e.Quarter == 2);

        if(searchDepartment == null)
            return products.Where(e =>
            e.Quarter == byte.Parse(searchQuarter) &&
            e.Year == short.Parse(searchYear));

        return products.Where(e => 
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
