using Microsoft.EntityFrameworkCore.Internal;
using PerformanceRatingSystem.Infrastructure.Initializers;

namespace PerformanceRatingSystem.WebMVC.Middleware;

public class DbInitializerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public Task Invoke(HttpContext context)
    {
        if (!(context.Session.Keys.Contains("starting")))
        {
            DbUserInitializer.Initialize(context).Wait();
            context.Session.SetString("starting", "Yes");
        }
        return _next.Invoke(context);
    }
}
