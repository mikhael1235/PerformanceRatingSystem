using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using PerformanceRatingSystem.Application.Dtos;
using PerformanceRatingSystem.Application.Requests.Queries;
using PerformanceRatingSystem.Application.Requests.Commands;
using PerformanceRatingSystem.Web.Controllers;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Tests.ControllersTests;

public class EmployeePerformanceIndicatorControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly EmployeePerformanceIndicatorController _controller;

    public EmployeePerformanceIndicatorControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new EmployeePerformanceIndicatorController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfEmployeePerformanceIndicators()
    {
        // Arrange
        var employeePerformanceIndicators = new PagedList<EmployeePerformanceIndicatorDto>([new(), new()], 2, 1, 2);
        EmployeePerformanceIndicatorParameters parameters = new();
        _mediatorMock
            .Setup(m => m.Send(new GetEmployeePerformanceIndicatorsQuery(parameters), CancellationToken.None))
            .ReturnsAsync(employeePerformanceIndicators);

        // Act
        var result = await _controller.Get(parameters);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<EmployeePerformanceIndicatorDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(employeePerformanceIndicators);

        _mediatorMock.Verify(m => m.Send(new GetEmployeePerformanceIndicatorsQuery(parameters), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingEmployeePerformanceIndicatorId_ReturnsEmployeePerformanceIndicator()
    {
        // Arrange
        var employeePerformanceIndicatorId = Guid.NewGuid();
        var employeePerformanceIndicator = new EmployeePerformanceIndicatorDto { IndicatorId = employeePerformanceIndicatorId };

        _mediatorMock
            .Setup(m => m.Send(new GetEmployeePerformanceIndicatorByIdQuery(employeePerformanceIndicatorId), CancellationToken.None))
            .ReturnsAsync(employeePerformanceIndicator);

        // Act
        var result = await _controller.GetById(employeePerformanceIndicatorId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as EmployeePerformanceIndicatorDto).Should().BeEquivalentTo(employeePerformanceIndicator);

        _mediatorMock.Verify(m => m.Send(new GetEmployeePerformanceIndicatorByIdQuery(employeePerformanceIndicatorId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingEmployeePerformanceIndicatorId_ReturnsNotFoundResult()
    {
        // Arrange
        var employeePerformanceIndicatorId = Guid.NewGuid();
        var employeePerformanceIndicator = new EmployeePerformanceIndicatorDto { IndicatorId = employeePerformanceIndicatorId };

        _mediatorMock
            .Setup(m => m.Send(new GetEmployeePerformanceIndicatorByIdQuery(employeePerformanceIndicatorId), CancellationToken.None))
            .ReturnsAsync((EmployeePerformanceIndicatorDto?)null);

        // Act
        var result = await _controller.GetById(employeePerformanceIndicatorId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetEmployeePerformanceIndicatorByIdQuery(employeePerformanceIndicatorId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_EmployeePerformanceIndicator_ReturnsEmployeePerformanceIndicator()
    {
        // Arrange
        var employeePerformanceIndicator = new EmployeePerformanceIndicatorForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateEmployeePerformanceIndicatorCommand(employeePerformanceIndicator), CancellationToken.None));

        // Act
        var result = await _controller.Create(employeePerformanceIndicator);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as EmployeePerformanceIndicatorForCreationDto).Should().BeEquivalentTo(employeePerformanceIndicator);

        _mediatorMock.Verify(m => m.Send(new CreateEmployeePerformanceIndicatorCommand(employeePerformanceIndicator), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateEmployeePerformanceIndicatorCommand(It.IsAny<EmployeePerformanceIndicatorForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingEmployeePerformanceIndicator_ReturnsNoContentResult()
    {
        // Arrange
        var employeePerformanceIndicatorId = Guid.NewGuid();
        var employeePerformanceIndicator = new EmployeePerformanceIndicatorForUpdateDto { Id = employeePerformanceIndicatorId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateEmployeePerformanceIndicatorCommand(employeePerformanceIndicator), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(employeePerformanceIndicatorId, employeePerformanceIndicator);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateEmployeePerformanceIndicatorCommand(employeePerformanceIndicator), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingEmployeePerformanceIndicator_ReturnsNotFoundResult()
    {
        // Arrange
        var employeePerformanceIndicatorId = Guid.NewGuid();
        var employeePerformanceIndicator = new EmployeePerformanceIndicatorForUpdateDto { Id = employeePerformanceIndicatorId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateEmployeePerformanceIndicatorCommand(employeePerformanceIndicator), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(employeePerformanceIndicatorId, employeePerformanceIndicator);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateEmployeePerformanceIndicatorCommand(employeePerformanceIndicator), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var employeePerformanceIndicatorId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(employeePerformanceIndicatorId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateEmployeePerformanceIndicatorCommand(It.IsAny<EmployeePerformanceIndicatorForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingEmployeePerformanceIndicatorId_ReturnsNoContentResult()
    {
        // Arrange
        var employeePerformanceIndicatorId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteEmployeePerformanceIndicatorCommand(employeePerformanceIndicatorId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(employeePerformanceIndicatorId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteEmployeePerformanceIndicatorCommand(employeePerformanceIndicatorId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingEmployeePerformanceIndicatorId_ReturnsNotFoundResult()
    {
        // Arrange
        var employeePerformanceIndicatorId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteEmployeePerformanceIndicatorCommand(employeePerformanceIndicatorId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(employeePerformanceIndicatorId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteEmployeePerformanceIndicatorCommand(employeePerformanceIndicatorId), CancellationToken.None), Times.Once);
    }
}

