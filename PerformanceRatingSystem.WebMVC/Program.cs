using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Application;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.WebMVC.Extensions;

namespace PerformanceRatingSystem.WebMVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddResponseCaching();
        builder.Services.Configure<ResponseCachingOptions>(options =>
        {
            options.MaximumBodySize = 1024 * 1024;
            options.UseCaseSensitivePaths = false;
        });
        builder.Services.ConfigureCors();
        builder.Services.AddControllersWithViews();
		builder.Services.AddRazorPages();
		builder.Services.ConfigureDbContext(builder.Configuration);
        builder.Services.ConfigureServices();
        builder.Services.AddAuthentication();
        builder.Services.ConfigureIdentity();

		builder.Services.AddDistributedMemoryCache();
		builder.Services.AddSession(options =>
		{
			options.Cookie.Name = ".PerformanceRatingSystem.Session";
			options.IdleTimeout = TimeSpan.FromSeconds(3600);
			options.Cookie.IsEssential = true;
		});

		builder.Services.Configure<CookiePolicyOptions>(options =>
		{

			options.CheckConsentNeeded = context => true;
			options.MinimumSameSitePolicy = SameSiteMode.None;
		});

		var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper autoMapper = mappingConfig.CreateMapper();
        builder.Services.AddSingleton(autoMapper);

        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(GetDepartmentsQuery).Assembly));



        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

		app.UseResponseCaching();
        app.UseHttpsRedirection();
        app.UseCors("CorsPolicy");
		app.UseCookiePolicy();
		app.UseSession();
        app.UseDbInitializer();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
		app.MapRazorPages();
		app.Run();
    }
}
