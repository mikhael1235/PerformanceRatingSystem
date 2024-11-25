using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Web.Controllers;

[Route("api/employeePerformanceIndicators")]
[ApiController]
public class EmployeePerformanceIndicatorController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeePerformanceIndicatorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] EmployeePerformanceIndicatorParameters parameters)
    {
        var employeePerformanceIndicators = await _mediator.Send(new GetEmployeePerformanceIndicatorsQuery(parameters));

        return Ok(employeePerformanceIndicators);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var employeePerformanceIndicator = await _mediator.Send(new GetEmployeePerformanceIndicatorByIdQuery(id));

        if (employeePerformanceIndicator is null)
        {
            return NotFound($"EmployeePerformanceIndicator with id {id} is not found.");
        }
        
        return Ok(employeePerformanceIndicator);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeePerformanceIndicatorForCreationDto? employeePerformanceIndicator)
    {
        if (employeePerformanceIndicator is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateEmployeePerformanceIndicatorCommand(employeePerformanceIndicator));

        return CreatedAtAction(nameof(Create), employeePerformanceIndicator);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployeePerformanceIndicatorForUpdateDto? employeePerformanceIndicator)
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

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteEmployeePerformanceIndicatorCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"EmployeePerformanceIndicator with id {id} is not found.");
        }

        return NoContent();
    }
}
