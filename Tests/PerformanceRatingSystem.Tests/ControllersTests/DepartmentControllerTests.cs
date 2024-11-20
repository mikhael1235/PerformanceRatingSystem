using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
using PerformanceRatingSystem.Web.Controllers;

namespace PerformanceRatingSystem.Tests.ControllersTests;

public class DepartmentControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly DepartmentController _controller;

    public DepartmentControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new DepartmentController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfDepartments()
    {
        // Arrange
        var departments = new List<DepartmentDto> { new(), new() };

        _mediatorMock
            .Setup(m => m.Send(new GetDepartmentsQuery(), CancellationToken.None))
            .ReturnsAsync(departments);

        // Act
        var result = await _controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<DepartmentDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(departments);

        _mediatorMock.Verify(m => m.Send(new GetDepartmentsQuery(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingDepartmentId_ReturnsDepartment()
    {
        // Arrange
        var departmentId = Guid.NewGuid();
        var department = new DepartmentDto { DepartmentId = departmentId };

        _mediatorMock
            .Setup(m => m.Send(new GetDepartmentByIdQuery(departmentId), CancellationToken.None))
            .ReturnsAsync(department);

        // Act
        var result = await _controller.GetById(departmentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as DepartmentDto).Should().BeEquivalentTo(department);

        _mediatorMock.Verify(m => m.Send(new GetDepartmentByIdQuery(departmentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingDepartmentId_ReturnsNotFoundResult()
    {
        // Arrange
        var departmentId = Guid.NewGuid();
        var department = new DepartmentDto { DepartmentId = departmentId };

        _mediatorMock
            .Setup(m => m.Send(new GetDepartmentByIdQuery(departmentId), CancellationToken.None))
            .ReturnsAsync((DepartmentDto?)null);

        // Act
        var result = await _controller.GetById(departmentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetDepartmentByIdQuery(departmentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Department_ReturnsDepartment()
    {
        // Arrange
        var department = new DepartmentForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateDepartmentCommand(department), CancellationToken.None));

        // Act
        var result = await _controller.Create(department);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as DepartmentForCreationDto).Should().BeEquivalentTo(department);

        _mediatorMock.Verify(m => m.Send(new CreateDepartmentCommand(department), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_NullValue_ReturnsBadRequest()
    {
        // Arrange and Act
        var result = await _controller.Create(null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new CreateDepartmentCommand(It.IsAny<DepartmentForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingDepartment_ReturnsNoContentResult()
    {
        // Arrange
        var departmentId = Guid.NewGuid();
        var department = new DepartmentForUpdateDto { Id = departmentId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateDepartmentCommand(department), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(departmentId, department);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateDepartmentCommand(department), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingDepartment_ReturnsNotFoundResult()
    {
        // Arrange
        var departmentId = Guid.NewGuid();
        var department = new DepartmentForUpdateDto { Id = departmentId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateDepartmentCommand(department), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(departmentId, department);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateDepartmentCommand(department), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var departmentId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(departmentId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateDepartmentCommand(It.IsAny<DepartmentForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingDepartmentId_ReturnsNoContentResult()
    {
        // Arrange
        var departmentId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteDepartmentCommand(departmentId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(departmentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteDepartmentCommand(departmentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingDepartmentId_ReturnsNotFoundResult()
    {
        // Arrange
        var departmentId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteDepartmentCommand(departmentId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(departmentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteDepartmentCommand(departmentId), CancellationToken.None), Times.Once);
    }
}

