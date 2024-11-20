using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Infrastructure;
using PerformanceRatingSystem.Infrastructure.Repositories;
using PerformanceRatingSystem.Domain.Abstractions;

namespace PerformanceRatingSystem.Web.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EmployeePerformanceContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DbConnection"), b =>
                b.MigrationsAssembly("PerformanceRatingSystem.Infrastructure")));
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
		services.AddScoped<IDepartmentRepository, DepartmentRepository>();
		services.AddScoped<IEmployeeRepository, EmployeeRepository>();
		services.AddScoped<IDepartmentPerformanceIndicatorRepository, DepartmentPerformanceIndicatorRepository>();
		services.AddScoped<IPlannedPerformanceValueRepository, PlannedPerformanceValueRepository>();
		services.AddScoped<IEmployeePerformanceIndicatorRepository, EmployeePerformanceIndicatorRepository>();
		services.AddScoped<IAchievementRepository, AchievementRepository>();
		services.AddScoped<IActualPerformanceResultRepository, ActualPerformanceResultRepository>();
    }
}
