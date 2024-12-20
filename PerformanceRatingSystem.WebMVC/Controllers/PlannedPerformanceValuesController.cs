using Microsoft.AspNetCore.Mvc;
using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceRatingSystem.Application.RequestHandlers.QueryHandlers;
using Microsoft.AspNetCore.Authorization;
using PerformanceRatingSystem.Domain.RequestFeatures;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace PerformanceRatingSystem.WebMVC.Controllers;

[Authorize]
public class PlannedPerformanceValuesController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index([FromQuery] PlannedPerformanceValueParameters parameters)
    {
        var pagedResult = await _mediator.Send(new GetPlannedPerformanceValuesQuery(parameters));
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));
        ViewData["SearchQuarter"] = parameters.SearchQuarter;
        ViewData["SearchYear"] = parameters.SearchYear;
        return View(pagedResult);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var plannedPerformanceValue = await _mediator.Send(new GetPlannedPerformanceValueByIdQuery(id));

        if (plannedPerformanceValue is null)
        {
            return NotFound($"PlannedPerformanceValue with id {id} is not found.");
        }

        return View(plannedPerformanceValue);
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create()
    {
        var indicators = await _mediator.Send(new GetDepartmentPerformanceIndicatorsQuery(new()
        {
            PageSize = 500
        }));

        if (indicators != null)
            ViewData["IndicatorId"] = new SelectList(indicators, "IndicatorId", "Name");

        return View();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] PlannedPerformanceValueForCreationDto? plannedPerformanceValue)
    {
        if (plannedPerformanceValue is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreatePlannedPerformanceValueCommand(plannedPerformanceValue));


        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var isEntityFound = await _mediator.Send(new GetPlannedPerformanceValueByIdQuery(id));
        if (isEntityFound == null)
        {
            return NotFound();
        }

        PlannedPerformanceValueForUpdateDto model = new()
        {
            IndicatorId = isEntityFound.IndicatorId,
            IndicatorType = isEntityFound.IndicatorType,
            Quarter = isEntityFound.Quarter,
            Value = isEntityFound.Value,
            Year = isEntityFound.Year,
        };

        var indicators = await _mediator.Send(new GetDepartmentPerformanceIndicatorsQuery(new()
        {
            PageSize = 500
        }));

        if (indicators != null)
            ViewData["IndicatorId"] = new SelectList(indicators, "IndicatorId", "Name");

        return View(model);
    }


    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] PlannedPerformanceValueForUpdateDto? plannedPerformanceValue)
    {
        if (plannedPerformanceValue is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdatePlannedPerformanceValueCommand(plannedPerformanceValue));

        if (!isEntityFound)
        {
            return NotFound($"PlannedPerformanceValue with id {id} is not found.");
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

        var achievement = await _mediator.Send(new GetPlannedPerformanceValueByIdQuery((Guid)id));

        return View(achievement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeletePlannedPerformanceValueCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"PlannedPerformanceValue with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));
    }
}
