namespace PerformanceRatingSystem.Application.Dtos;

public class AchievementForCreationDto 
{
	public string Description { get; set; }
	public DateOnly DateAchieved { get; set; }
	public Guid EmployeeId { get; set; }
}

