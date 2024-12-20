﻿using Microsoft.AspNetCore.Mvc;
using MediatR;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace PerformanceRatingSystem.WebMVC.Controllers;

[Authorize]
public class EmployeesController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index([FromQuery] EmployeeParameters parameters)
    {
        var pagedResult = await _mediator.Send(new GetEmployeesQuery(parameters));
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));
        ViewData["SearchPosition"] = parameters.SearchPosition;
        return View(pagedResult);
    }

    [HttpGet]
    public async Task<IActionResult> RatingByDepartment([FromQuery] ActualPerformanceResultParameters resultParameters)
    {
        var departments = await _mediator.Send(new GetAllDepartmentsQuery());

        if (departments != null)
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "Name");

        ViewData["SearchQuarter"] = resultParameters.SearchQuarter; 
        ViewData["SearchYear"] = resultParameters.SearchYear; 
        ViewData["SearchDepartment"] = resultParameters.SearchDepartment;

        var parameters = resultParameters;
        parameters.PageSize = 500;
        var employees = await _mediator.Send(new GetEmployeesByResultsQuery(parameters));
        if (employees == null)
        {
            return View();
        }

        return View(employees);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));

        if (employee is null)
        {
            return NotFound($"Employee with id {id} is not found.");
        }
        
        return View(employee);
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create()
    {
        var departments = await _mediator.Send(new GetAllDepartmentsQuery());

        if (departments != null) 
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "Name");

        return View();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] EmployeeForCreationDto? employee)
    {
        if (employee is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateEmployeeCommand(employee));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var isEntityFound = await _mediator.Send(new GetEmployeeByIdQuery(id));
        if (isEntityFound == null)
        {
            return NotFound();
        }

        var fullName = isEntityFound.FullName.Split(' ');
        EmployeeForUpdateDto model = new()
        {
            Surname = fullName[0],
            Name = fullName[1],
            Midname = fullName[2],
            DepartmentId = isEntityFound.DepartmentId,
            Position = isEntityFound.Position,
        };

        var departments = await _mediator.Send(new GetAllDepartmentsQuery());

        if (departments != null)
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "Name");

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] EmployeeForUpdateDto? employee)
    {
        if (employee is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateEmployeeCommand(employee));

        if (!isEntityFound)
        {
            return NotFound($"Employee with id {id} is not found.");
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

        var achievement = await _mediator.Send(new GetEmployeeByIdQuery((Guid)id));

        return View(achievement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteEmployeeCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Employee with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));
    }
}
