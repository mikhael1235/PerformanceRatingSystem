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

public class PlannedPerformanceValueControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly PlannedPerformanceValueController _controller;

    public PlannedPerformanceValueControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new PlannedPerformanceValueController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfPlannedPerformanceValues()
    {
        // Arrange
        var plannedPerformanceValues = new PagedList<PlannedPerformanceValueDto>([new(), new()], 2, 1, 2);
        PlannedPerformanceValueParameters parameters = new();
        _mediatorMock
            .Setup(m => m.Send(new GetPlannedPerformanceValuesQuery(parameters), CancellationToken.None))
            .ReturnsAsync(plannedPerformanceValues);

        // Act
        var result = await _controller.Get(parameters);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<PlannedPerformanceValueDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(plannedPerformanceValues);

        _mediatorMock.Verify(m => m.Send(new GetPlannedPerformanceValuesQuery(parameters), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingPlannedPerformanceValueId_ReturnsPlannedPerformanceValue()
    {
        // Arrange
        var plannedPerformanceValueId = Guid.NewGuid();
        var plannedPerformanceValue = new PlannedPerformanceValueDto { PlanId = plannedPerformanceValueId };

        _mediatorMock
            .Setup(m => m.Send(new GetPlannedPerformanceValueByIdQuery(plannedPerformanceValueId), CancellationToken.None))
            .ReturnsAsync(plannedPerformanceValue);

        // Act
        var result = await _controller.GetById(plannedPerformanceValueId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as PlannedPerformanceValueDto).Should().BeEquivalentTo(plannedPerformanceValue);

        _mediatorMock.Verify(m => m.Send(new GetPlannedPerformanceValueByIdQuery(plannedPerformanceValueId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingPlannedPerformanceValueId_ReturnsNotFoundResult()
    {
        // Arrange
        var plannedPerformanceValueId = Guid.NewGuid();
        var plannedPerformanceValue = new PlannedPerformanceValueDto { PlanId = plannedPerformanceValueId };

        _mediatorMock
            .Setup(m => m.Send(new GetPlannedPerformanceValueByIdQuery(plannedPerformanceValueId), CancellationToken.None))
            .ReturnsAsync((PlannedPerformanceValueDto?)null);

        // Act
        var result = await _controller.GetById(plannedPerformanceValueId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetPlannedPerformanceValueByIdQuery(plannedPerformanceValueId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_PlannedPerformanceValue_ReturnsPlannedPerformanceValue()
    {
        // Arrange
        var plannedPerformanceValue = new PlannedPerformanceValueForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreatePlannedPerformanceValueCommand(plannedPerformanceValue), CancellationToken.None));

        // Act
        var result = await _controller.Create(plannedPerformanceValue);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as PlannedPerformanceValueForCreationDto).Should().BeEquivalentTo(plannedPerformanceValue);

        _mediatorMock.Verify(m => m.Send(new CreatePlannedPerformanceValueCommand(plannedPerformanceValue), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreatePlannedPerformanceValueCommand(It.IsAny<PlannedPerformanceValueForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingPlannedPerformanceValue_ReturnsNoContentResult()
    {
        // Arrange
        var plannedPerformanceValueId = Guid.NewGuid();
        var plannedPerformanceValue = new PlannedPerformanceValueForUpdateDto { Id = plannedPerformanceValueId };

        _mediatorMock
            .Setup(m => m.Send(new UpdatePlannedPerformanceValueCommand(plannedPerformanceValue), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(plannedPerformanceValueId, plannedPerformanceValue);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdatePlannedPerformanceValueCommand(plannedPerformanceValue), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingPlannedPerformanceValue_ReturnsNotFoundResult()
    {
        // Arrange
        var plannedPerformanceValueId = Guid.NewGuid();
        var plannedPerformanceValue = new PlannedPerformanceValueForUpdateDto { Id = plannedPerformanceValueId };

        _mediatorMock
            .Setup(m => m.Send(new UpdatePlannedPerformanceValueCommand(plannedPerformanceValue), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(plannedPerformanceValueId, plannedPerformanceValue);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdatePlannedPerformanceValueCommand(plannedPerformanceValue), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var plannedPerformanceValueId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(plannedPerformanceValueId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdatePlannedPerformanceValueCommand(It.IsAny<PlannedPerformanceValueForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingPlannedPerformanceValueId_ReturnsNoContentResult()
    {
        // Arrange
        var plannedPerformanceValueId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeletePlannedPerformanceValueCommand(plannedPerformanceValueId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(plannedPerformanceValueId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeletePlannedPerformanceValueCommand(plannedPerformanceValueId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingPlannedPerformanceValueId_ReturnsNotFoundResult()
    {
        // Arrange
        var plannedPerformanceValueId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeletePlannedPerformanceValueCommand(plannedPerformanceValueId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(plannedPerformanceValueId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeletePlannedPerformanceValueCommand(plannedPerformanceValueId), CancellationToken.None), Times.Once);
    }
}

