using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Infrastructure.Extensions.Utility;
namespace PerformanceRatingSystem.Infrastructure.Extensions;

public static class RepositoryDepartmentExtensions
{
    public static IQueryable<Department> SearchByName(this IQueryable<Department> departments, string searchName)
    {
        if (string.IsNullOrWhiteSpace(searchName))
            return departments;

        var lowerCaseTerm = searchName.Trim().ToLower();

        return departments.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Department> Sort(this IQueryable<Department> departments, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return departments.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Department>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return departments.OrderBy(e => e.Name);

        return departments.OrderBy(orderQuery);
    }
}
