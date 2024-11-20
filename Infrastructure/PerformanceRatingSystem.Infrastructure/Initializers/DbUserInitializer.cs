using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PerformanceRatingSystem.Infrastructure.Initializers;

public static class DbUserInitializer
{
    public static async Task Initialize(HttpContext context)
    {
        UserManager<User> userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
        RoleManager<IdentityRole> roleManager = context.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();
        string adminEmail = "admin@gmail.com";
        string adminName = "admin@gmail.com";

        string password = "_Aa123456";
        if (await roleManager.FindByNameAsync("admin") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
        }
        if (await roleManager.FindByNameAsync("user") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("user"));
        }
        if (await userManager.FindByNameAsync(adminEmail) == null)
        {
            User admin = new()
            {
                Email = adminEmail,
                UserName = adminName,
                RegistrationDate = DateTime.Now
            };
            IdentityResult result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}