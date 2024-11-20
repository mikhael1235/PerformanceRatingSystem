using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Web.Controllers;

[Route("api/plannedPerformanceValues")]
[ApiController]
public class PlannedPerformanceValueController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlannedPerformanceValueController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var plannedPerformanceValues = await _mediator.Send(new GetPlannedPerformanceValuesQuery());

        return Ok(plannedPerformanceValues);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var plannedPerformanceValue = await _mediator.Send(new GetPlannedPerformanceValueByIdQuery(id));

        if (plannedPerformanceValue is null)
        {
            return NotFound($"PlannedPerformanceValue with id {id} is not found.");
        }
        
        return Ok(plannedPerformanceValue);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PlannedPerformanceValueForCreationDto? plannedPerformanceValue)
    {
        if (plannedPerformanceValue is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreatePlannedPerformanceValueCommand(plannedPerformanceValue));

        return CreatedAtAction(nameof(Create), plannedPerformanceValue);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PlannedPerformanceValueForUpdateDto? plannedPerformanceValue)
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

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeletePlannedPerformanceValueCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"PlannedPerformanceValue with id {id} is not found.");
        }

        return NoContent();
    }
}
