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

public class DepartmentPerformanceIndicatorControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly DepartmentPerformanceIndicatorController _controller;

    public DepartmentPerformanceIndicatorControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new DepartmentPerformanceIndicatorController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfDepartmentPerformanceIndicators()
    {
        // Arrange
        var departmentPerformanceIndicators = new PagedList<DepartmentPerformanceIndicatorDto>([new(), new()], 2, 1, 2);
        DepartmentPerformanceIndicatorParameters parameters = new();
        _mediatorMock
            .Setup(m => m.Send(new GetDepartmentPerformanceIndicatorsQuery(parameters), CancellationToken.None))
            .ReturnsAsync(departmentPerformanceIndicators);

        // Act
        var result = await _controller.Get(parameters);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<DepartmentPerformanceIndicatorDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(departmentPerformanceIndicators);

        _mediatorMock.Verify(m => m.Send(new GetDepartmentPerformanceIndicatorsQuery(parameters), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingDepartmentPerformanceIndicatorId_ReturnsDepartmentPerformanceIndicator()
    {
        // Arrange
        var departmentPerformanceIndicatorId = Guid.NewGuid();
        var departmentPerformanceIndicator = new DepartmentPerformanceIndicatorDto { IndicatorId = departmentPerformanceIndicatorId };

        _mediatorMock
            .Setup(m => m.Send(new GetDepartmentPerformanceIndicatorByIdQuery(departmentPerformanceIndicatorId), CancellationToken.None))
            .ReturnsAsync(departmentPerformanceIndicator);

        // Act
        var result = await _controller.GetById(departmentPerformanceIndicatorId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as DepartmentPerformanceIndicatorDto).Should().BeEquivalentTo(departmentPerformanceIndicator);

        _mediatorMock.Verify(m => m.Send(new GetDepartmentPerformanceIndicatorByIdQuery(departmentPerformanceIndicatorId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingDepartmentPerformanceIndicatorId_ReturnsNotFoundResult()
    {
        // Arrange
        var departmentPerformanceIndicatorId = Guid.NewGuid();
        var departmentPerformanceIndicator = new DepartmentPerformanceIndicatorDto { IndicatorId = departmentPerformanceIndicatorId };

        _mediatorMock
            .Setup(m => m.Send(new GetDepartmentPerformanceIndicatorByIdQuery(departmentPerformanceIndicatorId), CancellationToken.None))
            .ReturnsAsync((DepartmentPerformanceIndicatorDto?)null);

        // Act
        var result = await _controller.GetById(departmentPerformanceIndicatorId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetDepartmentPerformanceIndicatorByIdQuery(departmentPerformanceIndicatorId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_DepartmentPerformanceIndicator_ReturnsDepartmentPerformanceIndicator()
    {
        // Arrange
        var departmentPerformanceIndicator = new DepartmentPerformanceIndicatorForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicator), CancellationToken.None));

        // Act
        var result = await _controller.Create(departmentPerformanceIndicator);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as DepartmentPerformanceIndicatorForCreationDto).Should().BeEquivalentTo(departmentPerformanceIndicator);

        _mediatorMock.Verify(m => m.Send(new CreateDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicator), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateDepartmentPerformanceIndicatorCommand(It.IsAny<DepartmentPerformanceIndicatorForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingDepartmentPerformanceIndicator_ReturnsNoContentResult()
    {
        // Arrange
        var departmentPerformanceIndicatorId = Guid.NewGuid();
        var departmentPerformanceIndicator = new DepartmentPerformanceIndicatorForUpdateDto { Id = departmentPerformanceIndicatorId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicator), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(departmentPerformanceIndicatorId, departmentPerformanceIndicator);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicator), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingDepartmentPerformanceIndicator_ReturnsNotFoundResult()
    {
        // Arrange
        var departmentPerformanceIndicatorId = Guid.NewGuid();
        var departmentPerformanceIndicator = new DepartmentPerformanceIndicatorForUpdateDto { Id = departmentPerformanceIndicatorId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicator), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(departmentPerformanceIndicatorId, departmentPerformanceIndicator);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicator), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var departmentPerformanceIndicatorId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(departmentPerformanceIndicatorId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateDepartmentPerformanceIndicatorCommand(It.IsAny<DepartmentPerformanceIndicatorForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingDepartmentPerformanceIndicatorId_ReturnsNoContentResult()
    {
        // Arrange
        var departmentPerformanceIndicatorId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicatorId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(departmentPerformanceIndicatorId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicatorId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingDepartmentPerformanceIndicatorId_ReturnsNotFoundResult()
    {
        // Arrange
        var departmentPerformanceIndicatorId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicatorId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(departmentPerformanceIndicatorId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteDepartmentPerformanceIndicatorCommand(departmentPerformanceIndicatorId), CancellationToken.None), Times.Once);
    }
}

