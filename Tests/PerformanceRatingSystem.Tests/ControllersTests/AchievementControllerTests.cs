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

public class AchievementControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AchievementController _controller;

    public AchievementControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new AchievementController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfAchievements()
    {
        // Arrange
        var achievements = new PagedList<AchievementDto> ( [new(), new()],2,1,2);
        AchievementParameters parameters = new();
        _mediatorMock
            .Setup(m => m.Send(new GetAchievementsQuery(parameters), CancellationToken.None))
            .ReturnsAsync(achievements);

        // Act
        var result = await _controller.Get(parameters);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<AchievementDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(achievements);

        _mediatorMock.Verify(m => m.Send(new GetAchievementsQuery(parameters), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingAchievementId_ReturnsAchievement()
    {
        // Arrange
        var achievementId = Guid.NewGuid();
        var achievement = new AchievementDto { AchievementId = achievementId };

        _mediatorMock
            .Setup(m => m.Send(new GetAchievementByIdQuery(achievementId), CancellationToken.None))
            .ReturnsAsync(achievement);

        // Act
        var result = await _controller.GetById(achievementId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as AchievementDto).Should().BeEquivalentTo(achievement);

        _mediatorMock.Verify(m => m.Send(new GetAchievementByIdQuery(achievementId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingAchievementId_ReturnsNotFoundResult()
    {
        // Arrange
        var achievementId = Guid.NewGuid();
        var achievement = new AchievementDto { AchievementId = achievementId };

        _mediatorMock
            .Setup(m => m.Send(new GetAchievementByIdQuery(achievementId), CancellationToken.None))
            .ReturnsAsync((AchievementDto?)null);

        // Act
        var result = await _controller.GetById(achievementId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetAchievementByIdQuery(achievementId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Achievement_ReturnsAchievement()
    {
        // Arrange
        var achievement = new AchievementForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateAchievementCommand(achievement), CancellationToken.None));

        // Act
        var result = await _controller.Create(achievement);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as AchievementForCreationDto).Should().BeEquivalentTo(achievement);

        _mediatorMock.Verify(m => m.Send(new CreateAchievementCommand(achievement), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateAchievementCommand(It.IsAny<AchievementForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingAchievement_ReturnsNoContentResult()
    {
        // Arrange
        var achievementId = Guid.NewGuid();
        var achievement = new AchievementForUpdateDto { Id = achievementId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateAchievementCommand(achievement), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(achievementId, achievement);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateAchievementCommand(achievement), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingAchievement_ReturnsNotFoundResult()
    {
        // Arrange
        var achievementId = Guid.NewGuid();
        var achievement = new AchievementForUpdateDto { Id = achievementId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateAchievementCommand(achievement), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(achievementId, achievement);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateAchievementCommand(achievement), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var achievementId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(achievementId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateAchievementCommand(It.IsAny<AchievementForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingAchievementId_ReturnsNoContentResult()
    {
        // Arrange
        var achievementId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteAchievementCommand(achievementId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(achievementId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteAchievementCommand(achievementId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingAchievementId_ReturnsNotFoundResult()
    {
        // Arrange
        var achievementId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteAchievementCommand(achievementId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(achievementId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteAchievementCommand(achievementId), CancellationToken.None), Times.Once);
    }
}

