using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;

namespace PerformanceRatingSystem.Web.Controllers;

[Route("api/achievements")]
[ApiController]
public class AchievementController : ControllerBase
{
    private readonly IMediator _mediator;

    public AchievementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var achievements = await _mediator.Send(new GetAchievementsQuery());

        return Ok(achievements);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var achievement = await _mediator.Send(new GetAchievementByIdQuery(id));

        if (achievement is null)
        {
            return NotFound($"Achievement with id {id} is not found.");
        }
        
        return Ok(achievement);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AchievementForCreationDto? achievement)
    {
        if (achievement is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateAchievementCommand(achievement));

        return CreatedAtAction(nameof(Create), achievement);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AchievementForUpdateDto? achievement)
    {
        if (achievement is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateAchievementCommand(achievement));

        if (!isEntityFound)
        {
            return NotFound($"Achievement with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteAchievementCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Achievement with id {id} is not found.");
        }

        return NoContent();
    }
}
