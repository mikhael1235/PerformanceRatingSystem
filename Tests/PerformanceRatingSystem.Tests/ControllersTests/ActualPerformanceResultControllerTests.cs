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

public class ActualPerformanceResultControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ActualPerformanceResultController _controller;

    public ActualPerformanceResultControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ActualPerformanceResultController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfActualPerformanceResults()
    {
        // Arrange
        var actualPerformanceResults = new PagedList<ActualPerformanceResultDto>([new(), new()], 2, 1, 5);
        ActualPerformanceResultParameters parameters = new();

        _mediatorMock
            .Setup(m => m.Send(new GetActualPerformanceResultsQuery(parameters), CancellationToken.None))
            .ReturnsAsync(actualPerformanceResults);

        // Act
        var result = await _controller.Get(parameters);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<ActualPerformanceResultDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(actualPerformanceResults);

        _mediatorMock.Verify(m => m.Send(new GetActualPerformanceResultsQuery(parameters), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingActualPerformanceResultId_ReturnsActualPerformanceResult()
    {
        // Arrange
        var actualPerformanceResultId = Guid.NewGuid();
        var actualPerformanceResult = new ActualPerformanceResultDto { ResultId = actualPerformanceResultId };

        _mediatorMock
            .Setup(m => m.Send(new GetActualPerformanceResultByIdQuery(actualPerformanceResultId), CancellationToken.None))
            .ReturnsAsync(actualPerformanceResult);

        // Act
        var result = await _controller.GetById(actualPerformanceResultId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as ActualPerformanceResultDto).Should().BeEquivalentTo(actualPerformanceResult);

        _mediatorMock.Verify(m => m.Send(new GetActualPerformanceResultByIdQuery(actualPerformanceResultId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingActualPerformanceResultId_ReturnsNotFoundResult()
    {
        // Arrange
        var actualPerformanceResultId = Guid.NewGuid();
        var actualPerformanceResult = new ActualPerformanceResultDto { ResultId = actualPerformanceResultId };

        _mediatorMock
            .Setup(m => m.Send(new GetActualPerformanceResultByIdQuery(actualPerformanceResultId), CancellationToken.None))
            .ReturnsAsync((ActualPerformanceResultDto?)null);

        // Act
        var result = await _controller.GetById(actualPerformanceResultId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetActualPerformanceResultByIdQuery(actualPerformanceResultId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_ActualPerformanceResult_ReturnsActualPerformanceResult()
    {
        // Arrange
        var actualPerformanceResult = new ActualPerformanceResultForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateActualPerformanceResultCommand(actualPerformanceResult), CancellationToken.None));

        // Act
        var result = await _controller.Create(actualPerformanceResult);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as ActualPerformanceResultForCreationDto).Should().BeEquivalentTo(actualPerformanceResult);

        _mediatorMock.Verify(m => m.Send(new CreateActualPerformanceResultCommand(actualPerformanceResult), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateActualPerformanceResultCommand(It.IsAny<ActualPerformanceResultForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingActualPerformanceResult_ReturnsNoContentResult()
    {
        // Arrange
        var actualPerformanceResultId = Guid.NewGuid();
        var actualPerformanceResult = new ActualPerformanceResultForUpdateDto { Id = actualPerformanceResultId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateActualPerformanceResultCommand(actualPerformanceResult), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(actualPerformanceResultId, actualPerformanceResult);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateActualPerformanceResultCommand(actualPerformanceResult), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingActualPerformanceResult_ReturnsNotFoundResult()
    {
        // Arrange
        var actualPerformanceResultId = Guid.NewGuid();
        var actualPerformanceResult = new ActualPerformanceResultForUpdateDto { Id = actualPerformanceResultId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateActualPerformanceResultCommand(actualPerformanceResult), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(actualPerformanceResultId, actualPerformanceResult);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateActualPerformanceResultCommand(actualPerformanceResult), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var actualPerformanceResultId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(actualPerformanceResultId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateActualPerformanceResultCommand(It.IsAny<ActualPerformanceResultForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingActualPerformanceResultId_ReturnsNoContentResult()
    {
        // Arrange
        var actualPerformanceResultId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteActualPerformanceResultCommand(actualPerformanceResultId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(actualPerformanceResultId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteActualPerformanceResultCommand(actualPerformanceResultId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingActualPerformanceResultId_ReturnsNotFoundResult()
    {
        // Arrange
        var actualPerformanceResultId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteActualPerformanceResultCommand(actualPerformanceResultId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(actualPerformanceResultId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteActualPerformanceResultCommand(actualPerformanceResultId), CancellationToken.None), Times.Once);
    }
}

