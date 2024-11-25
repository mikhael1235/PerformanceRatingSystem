using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Infrastructure.Extensions.Utility;
namespace PerformanceRatingSystem.Infrastructure.Extensions;

public static class RepositoryEmployeePerformanceIndicatorPerformanceIndicatorExtensions
{
    public static IQueryable<EmployeePerformanceIndicator> SearchByName(this IQueryable<EmployeePerformanceIndicator> employeePerformanceIndicators, string searchName)
    {
        if (string.IsNullOrWhiteSpace(searchName))
            return employeePerformanceIndicators;

        var lowerCaseTerm = searchName.Trim().ToLower();

        return employeePerformanceIndicators.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<EmployeePerformanceIndicator> Sort(this IQueryable<EmployeePerformanceIndicator> employeePerformanceIndicators, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return employeePerformanceIndicators.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<EmployeePerformanceIndicator>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return employeePerformanceIndicators.OrderBy(e => e.Name);

        return employeePerformanceIndicators.OrderBy(orderQuery);
    }
}
