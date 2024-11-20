using PerformanceRatingSystem.WebMVC.Middleware;

namespace PerformanceRatingSystem.WebMVC.Extensions;

public static class DbInitializerExtensions
{
    public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DbInitializerMiddleware>();
    }

}
