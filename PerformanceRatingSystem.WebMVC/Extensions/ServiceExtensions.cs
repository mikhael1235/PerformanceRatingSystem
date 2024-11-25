using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Infrastructure.Repositories;
using PerformanceRatingSystem.Infrastructure;
using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.WebMVC.Extensions;

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
            opts.UseSqlServer(configuration.GetConnectionString("MyConnection"), b =>
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

    public static void ConfigureIdentity(this IServiceCollection services)
    {
		services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
					 .AddEntityFrameworkStores<EmployeePerformanceContext>()
                     .AddDefaultUI()
					 .AddDefaultTokenProviders();
	}
}
