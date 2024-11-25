﻿using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace PerformanceRatingSystem.WebMVC.Controllers;

[Authorize]
public class ActualPerformanceResultsController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
 
    [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index()
    {
        var actualPerformanceResults = await _mediator.Send(new GetActualPerformanceResultsQuery());

        return View(actualPerformanceResults);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var actualPerformanceResult = await _mediator.Send(new GetActualPerformanceResultByIdQuery(id));

        if (actualPerformanceResult is null)
        {
            return NotFound($"ActualPerformanceResult with id {id} is not found.");
        }
        
        return View(actualPerformanceResult);
    }
 

 
    [HttpGet]
    public async Task<IActionResult> Create()
 
    {
 
        var indicators = await _mediator.Send(new GetEmployeePerformanceIndicatorsQuery());
 

 
        if (indicators != null)
 
            ViewData["IndicatorId"] = new SelectList(indicators, "IndicatorId", "Name");
 

 
        return View();
 
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] ActualPerformanceResultForCreationDto? actualPerformanceResult)
    {
        if (actualPerformanceResult is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateActualPerformanceResultCommand(actualPerformanceResult));

        return RedirectToAction(nameof(Index));
    }
 

 
    [HttpGet]
 
    public async Task<IActionResult> Edit(Guid id)
 
    {
 
        var isEntityFound = await _mediator.Send(new GetActualPerformanceResultByIdQuery(id));
 
        if (isEntityFound == null)
 
        {
 
            return NotFound();
 
        }
 

 
        ActualPerformanceResultForUpdateDto model = new()
 
        {
 
            IndicatorId = isEntityFound.IndicatorId,
 
            IndicatorType = isEntityFound.IndicatorType,
 
            Quarter = isEntityFound.Quarter,
 
            Value = isEntityFound.Value,
 
            Year = isEntityFound.Year,
 
        };
 

 
        var indicators = await _mediator.Send(new GetEmployeePerformanceIndicatorsQuery());
 

 
        if (indicators != null)
 
            ViewData["IndicatorId"] = new SelectList(indicators, "IndicatorId", "Name");
 

 
        return View(model);
 
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] ActualPerformanceResultForUpdateDto? actualPerformanceResult)
    {
        if (actualPerformanceResult is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateActualPerformanceResultCommand(actualPerformanceResult));

        if (!isEntityFound)
        {
            return NotFound($"ActualPerformanceResult with id {id} is not found.");
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
 

 
        var achievement = await _mediator.Send(new GetActualPerformanceResultByIdQuery((Guid)id));
 

 
        return View(achievement);
 
    }
 

 
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteActualPerformanceResultCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"ActualPerformanceResult with id {id} is not found.");
        }
 

 
        return RedirectToAction(nameof(Index));

    }
}
 
 