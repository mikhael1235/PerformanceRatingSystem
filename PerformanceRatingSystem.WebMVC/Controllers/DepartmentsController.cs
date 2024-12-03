using Microsoft.AspNetCore.Mvc;
using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
using PerformanceRatingSystem.Domain.Entities;
using Azure.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceRatingSystem.Domain.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
namespace PerformanceRatingSystem.WebMVC.Controllers;

[Authorize]
public class DepartmentsController : Controller
{
    private readonly IMediator _mediator;
    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index([FromQuery] DepartmentParameters parameters)
    {
        var pagedResult = await _mediator.Send(new GetDepartmentsQuery(parameters));
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));
        ViewData["SearchName"] = parameters.SearchName;
        return View(pagedResult);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var department = await _mediator.Send(new GetDepartmentByIdQuery(id));

        if (department is null)
        {
            return NotFound($"Department with id {id} is not found.");
        }

        return View(department);
    }

    [HttpGet]
    public async Task<IActionResult> Rating([FromQuery] ActualPerformanceResultParameters resultParameters)
    {
        var parameters = resultParameters;
        parameters.PageSize = 500;
        var departments = await _mediator.Send(new GetDepartmentsByResultsQuery(parameters));
        ViewData["SearchQuarter"] = resultParameters.SearchQuarter;
        ViewData["SearchYear"] = resultParameters.SearchYear;
        if (departments == null)
        {
            return View();
        }

        return View(departments);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] DepartmentForCreationDto? department)
    {
        if (department is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateDepartmentCommand(department));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var isEntityFound = await _mediator.Send(new GetDepartmentByIdQuery(id));
        if (isEntityFound == null)
        {
            return NotFound();
        }

        DepartmentForUpdateDto model =   new()
        {
            Name = isEntityFound.Name,
        };

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] DepartmentForUpdateDto? department)
    {
        if (department is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateDepartmentCommand(department));

        if (!isEntityFound)
        {
            return NotFound($"Department with id {id} is not found.");
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

        var achievement = await _mediator.Send(new GetDepartmentByIdQuery((Guid)id));

        return View(achievement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteDepartmentCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Department with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));

    }
}
