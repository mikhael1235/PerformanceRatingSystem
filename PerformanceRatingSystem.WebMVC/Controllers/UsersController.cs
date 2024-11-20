using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.WebMVC.Controllers;

[Authorize(Roles = "admin")]
public class UsersController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager) : Controller
{
    private readonly UserManager<User> _userManager = userManager;
    readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.OrderBy(user => user.Id).ToListAsync();

        List<UserDto> userDtos = [];

        string urole = "";
        foreach (var user in users)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Count > 0)
            {
                urole = userRoles[0] ?? "";
            }

            userDtos.Add(
                new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    RegistrationDate = user.RegistrationDate,
                    RoleName = urole

                });

        }

        return View(userDtos);
    }

    public IActionResult Create()
    {
        var allRoles = _roleManager.Roles.ToList();
        UserForCreationDto user = new();

        ViewData["UserRole"] = new SelectList(allRoles, "Name", "Name");

        return View(user);

    }

    [HttpPost]
    public async Task<IActionResult> Create(UserForCreationDto model)
    {
        if (ModelState.IsValid)
        {
            User user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                RegistrationDate = model.RegistrationDate,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            var role = model.UserRole;
            if (role.Length > 0)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(string id)
    {
        User user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles.ToList();
        string userRole = "";
        if (userRoles.Count > 0)
        {
            userRole = userRoles[0] ?? "";
        }

        UserForUpdateDto model = new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.UserName,
            RegistrationDate = user.RegistrationDate,
            UserRole = userRole
        };
        ViewData["UserRole"] = new SelectList(allRoles, "Name", "Name", model.UserRole);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserForUpdateDto model)
    {
        if (ModelState.IsValid)
        {
            User user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                var oldRoles = await _userManager.GetRolesAsync(user);

                if (oldRoles.Count > 0)
                {
                    await _userManager.RemoveFromRolesAsync(user, oldRoles);

                }
                var newRole = model.UserRole;
                if (newRole.Length > 0)
                {
                    await _userManager.AddToRoleAsync(user, newRole);
                }
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.RegistrationDate = model.RegistrationDate;


                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
        }
        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Delete(string id)
    {
        User user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
        }
        return RedirectToAction("Index");
    }
}
