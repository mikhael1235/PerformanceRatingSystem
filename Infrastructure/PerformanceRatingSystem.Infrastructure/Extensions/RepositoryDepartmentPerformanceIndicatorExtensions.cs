using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Infrastructure.Extensions.Utility;
namespace PerformanceRatingSystem.Infrastructure.Extensions;

public static class RepositoryDepartmentPerformanceIndicatorPerformanceIndicatorExtensions
{
    public static IQueryable<DepartmentPerformanceIndicator> SearchByName(this IQueryable<DepartmentPerformanceIndicator> departmentPerformanceIndicators, string searchName)
    {
        if (string.IsNullOrWhiteSpace(searchName))
            return departmentPerformanceIndicators;

        var lowerCaseTerm = searchName.Trim().ToLower();

        return departmentPerformanceIndicators.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<DepartmentPerformanceIndicator> Sort(this IQueryable<DepartmentPerformanceIndicator> departmentPerformanceIndicators, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return departmentPerformanceIndicators.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<DepartmentPerformanceIndicator>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return departmentPerformanceIndicators.OrderBy(e => e.Name);

        return departmentPerformanceIndicators.OrderBy(orderQuery);
    }
}
