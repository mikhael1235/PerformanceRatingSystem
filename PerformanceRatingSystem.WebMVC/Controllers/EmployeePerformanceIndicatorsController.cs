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

namespace PerformanceRatingSystem.WebMVC.Controllers;

[Authorize]
public class EmployeePerformanceIndicatorsController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index([FromQuery] EmployeePerformanceIndicatorParameters parameters)
    {
        var pagedResult = await _mediator.Send(new GetEmployeePerformanceIndicatorsQuery(parameters));
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));
        ViewData["SearchName"] = parameters.SearchName;
        return View(pagedResult);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var employeePerformanceIndicator = await _mediator.Send(new GetEmployeePerformanceIndicatorByIdQuery(id));

        if (employeePerformanceIndicator is null)
        {
            return NotFound($"EmployeePerformanceIndicator with id {id} is not found.");
        }
        
        return View(employeePerformanceIndicator);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var employees = await _mediator.Send(new GetEmployeesQuery(new()
        {
            PageSize = 500
        }));

        if (employees != null)
            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "FullName");

        return View();
    }


    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] EmployeePerformanceIndicatorForCreationDto? employeePerformanceIndicator)
    {
        if (employeePerformanceIndicator is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateEmployeePerformanceIndicatorCommand(employeePerformanceIndicator));


        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var isEntityFound = await _mediator.Send(new GetEmployeePerformanceIndicatorByIdQuery(id));
        if (isEntityFound == null)
        {
            return NotFound();
        }

        EmployeePerformanceIndicatorForUpdateDto model = new()
        {
            Name = isEntityFound.Name,
            EmployeeId = isEntityFound.EmployeeId,
        };

        var employees = await _mediator.Send(new GetEmployeesQuery(new()
        {
            PageSize = 500
        }));

        if (employees != null)
            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "FullName");

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] EmployeePerformanceIndicatorForUpdateDto? employeePerformanceIndicator)
    {
        if (employeePerformanceIndicator is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateEmployeePerformanceIndicatorCommand(employeePerformanceIndicator));

        if (!isEntityFound)
        {
            return NotFound($"EmployeePerformanceIndicator with id {id} is not found.");
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

        var achievement = await _mediator.Send(new GetEmployeePerformanceIndicatorByIdQuery((Guid)id));

        return View(achievement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteEmployeePerformanceIndicatorCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"EmployeePerformanceIndicator with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));
    }
}
