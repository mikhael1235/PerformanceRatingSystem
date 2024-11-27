using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Infrastructure.Extensions.Utility;

namespace PerformanceRatingSystem.Infrastructure.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> SearchByPosition(this IQueryable<Employee> employees, string searchPosition)
    {
        if (string.IsNullOrWhiteSpace(searchPosition))
            return employees;

        var lowerCaseTerm = searchPosition.Trim().ToLower();

        return employees.Where(e => e.Position.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return employees.OrderBy(e => e.Surname);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return employees.OrderBy(e => e.Surname);

        return employees.OrderBy(orderQuery);
    }
}
