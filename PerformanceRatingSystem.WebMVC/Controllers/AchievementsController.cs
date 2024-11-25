using Microsoft.AspNetCore.Mvc;
 
using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
 
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using PerformanceRatingSystem.Domain.RequestFeatures;
using System.Linq.Dynamic.Core;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace PerformanceRatingSystem.WebMVC.Controllers;

[Authorize]
public class AchievementsController : Controller
{
    private readonly IMediator _mediator;

    public AchievementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
 
    [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index([FromQuery] AchievementParameters parameters)
    {
        var pagedResult = await _mediator.Send(new GetAchievementsQuery(parameters));
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));
        ViewData["SearchDescription"] = parameters.SearchDescription;
        return View(pagedResult);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var achievement = await _mediator.Send(new GetAchievementByIdQuery(id));

        if (achievement is null)
        {
            return NotFound($"Achievement with id {id} is not found.");
        }
 

 
        return View(achievement);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
 
    {
 
        var employees = await _mediator.Send(new GetEmployeesQuery(new()));
 

 
        if (employees != null)
 
            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "FullName");
 

 
        return View();
 
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] AchievementForCreationDto? achievement)
    {
        if (achievement is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateAchievementCommand(achievement));

        return RedirectToAction(nameof(Index));
    }
 

 
    [HttpGet]

 
    public async Task<IActionResult> Edit(Guid id)
 
    {
 
        var isEntityFound = await _mediator.Send(new GetAchievementByIdQuery(id));
 
        if (isEntityFound == null)
 
        {
 
            return NotFound();
 
        }
 

 
        AchievementForUpdateDto model = new()
 
        {
 
            DateAchieved = isEntityFound.DateAchieved,
 
            Description = isEntityFound.Description,
 
            EmployeeId = isEntityFound.EmployeeId,
 
        };
 

 
        var employees = await _mediator.Send(new GetEmployeesQuery(new()));
 
        ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "FullName");
 

 
        return View(model);
 
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] AchievementForUpdateDto? achievement)
    {
        if (achievement is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateAchievementCommand(achievement));

        if (!isEntityFound)
        {
            return NotFound($"Achievement with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));
    }
 

 
    [HttpGet]
 
    public async Task<IActionResult> Delete(Guid? id)
 
    {
 
        if (id == null)
 
        {
 
            return NotFound();
 
        }
 

 
        var achievement = await _mediator.Send(new GetAchievementByIdQuery((Guid)id));
 

 
        return View(achievement);
 
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteAchievementCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Achievement with id {id} is not found.");
        }
        return RedirectToAction(nameof(Index));

    }
}
 
 