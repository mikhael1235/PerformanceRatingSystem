using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Web.Controllers;

[Route("api/actualPerformanceResults")]
[ApiController]
public class ActualPerformanceResultController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActualPerformanceResultController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ActualPerformanceResultParameters parameters)
    {
        var actualPerformanceResults = await _mediator.Send(new GetActualPerformanceResultsQuery(parameters));

        return Ok(actualPerformanceResults);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var actualPerformanceResult = await _mediator.Send(new GetActualPerformanceResultByIdQuery(id));

        if (actualPerformanceResult is null)
        {
            return NotFound($"ActualPerformanceResult with id {id} is not found.");
        }
        
        return Ok(actualPerformanceResult);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ActualPerformanceResultForCreationDto? actualPerformanceResult)
    {
        if (actualPerformanceResult is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateActualPerformanceResultCommand(actualPerformanceResult));

        return CreatedAtAction(nameof(Create), actualPerformanceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ActualPerformanceResultForUpdateDto? actualPerformanceResult)
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

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteActualPerformanceResultCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"ActualPerformanceResult with id {id} is not found.");
        }

        return NoContent();
    }
}
