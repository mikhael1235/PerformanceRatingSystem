using Microsoft.AspNetCore.Mvc;
using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using PerformanceRatingSystem.Domain.RequestFeatures;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace PerformanceRatingSystem.Web.Controllers;

[Authorize]
public class DepartmentPerformanceIndicatorsController : Controller
{
    private readonly IMediator _mediator;

    public DepartmentPerformanceIndicatorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index([FromQuery] DepartmentPerformanceIndicatorParameters parameters)
    {
        var pagedResult = await _mediator.Send(new GetDepartmentPerformanceIndicatorsQuery(parameters));
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));
        ViewData["SearchName"] = parameters.SearchName;
        return View(pagedResult);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var departmentPerformanceIndicator = await _mediator.Send(new GetDepartmentPerformanceIndicatorByIdQuery(id));

        if (departmentPerformanceIndicator is null)
        {
            return NotFound($"DepartmentPerformanceIndicator with id {id} is not found.");
        }
        
        return View(departmentPerformanceIndicator);
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create()
    {
        var departments = await _mediator.Send(new GetAllDepartmentsQuery());

        if (departments != null)
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "Name");

        return View();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] DepartmentPerformanceIndicatorForCreationDto? departmentPerformanceIndicator)
    {
        if (departmentPerformanceIndicator is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicator));


        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var isEntityFound = await _mediator.Send(new GetDepartmentPerformanceIndicatorByIdQuery(id));
        if (isEntityFound == null)
        {
            return NotFound();
        }

        DepartmentPerformanceIndicatorForUpdateDto model = new()
        {
            Name = isEntityFound.Name,
            DepartmentId = isEntityFound.DepartmentId,
        };

        var departments = await _mediator.Send(new GetAllDepartmentsQuery());

        if (departments != null)
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "Name");

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] DepartmentPerformanceIndicatorForUpdateDto? departmentPerformanceIndicator)
    {
        if (departmentPerformanceIndicator is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicator));

        if (!isEntityFound)
        {
            return NotFound($"DepartmentPerformanceIndicator with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var achievement = await _mediator.Send(new GetDepartmentPerformanceIndicatorByIdQuery((Guid)id));

        return View(achievement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteDepartmentPerformanceIndicatorCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"DepartmentPerformanceIndicator with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));

    }
}
