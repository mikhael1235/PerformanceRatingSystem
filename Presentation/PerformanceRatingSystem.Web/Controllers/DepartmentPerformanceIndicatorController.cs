using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Web.Controllers;

[Route("api/departmentPerformanceIndicators")]
[ApiController]
public class DepartmentPerformanceIndicatorController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentPerformanceIndicatorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] DepartmentPerformanceIndicatorParameters parameters)
    {
        var departmentPerformanceIndicators = await _mediator.Send(new GetDepartmentPerformanceIndicatorsQuery(parameters));

        return Ok(departmentPerformanceIndicators);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var departmentPerformanceIndicator = await _mediator.Send(new GetDepartmentPerformanceIndicatorByIdQuery(id));

        if (departmentPerformanceIndicator is null)
        {
            return NotFound($"DepartmentPerformanceIndicator with id {id} is not found.");
        }
        
        return Ok(departmentPerformanceIndicator);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DepartmentPerformanceIndicatorForCreationDto? departmentPerformanceIndicator)
    {
        if (departmentPerformanceIndicator is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicator));

        return CreatedAtAction(nameof(Create), departmentPerformanceIndicator);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DepartmentPerformanceIndicatorForUpdateDto? departmentPerformanceIndicator)
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

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteDepartmentPerformanceIndicatorCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"DepartmentPerformanceIndicator with id {id} is not found.");
        }

        return NoContent();
    }
}
