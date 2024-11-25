using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Web.Controllers;

[Route("api/departments")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] DepartmentParameters parameters)
    {
        var departments = await _mediator.Send(new GetDepartmentsQuery(parameters));

        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var department = await _mediator.Send(new GetDepartmentByIdQuery(id));

        if (department is null)
        {
            return NotFound($"Department with id {id} is not found.");
        }
        
        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DepartmentForCreationDto? department)
    {
        if (department is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateDepartmentCommand(department));

        return CreatedAtAction(nameof(Create), department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DepartmentForUpdateDto? department)
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

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteDepartmentCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Department with id {id} is not found.");
        }

        return NoContent();
    }
}
