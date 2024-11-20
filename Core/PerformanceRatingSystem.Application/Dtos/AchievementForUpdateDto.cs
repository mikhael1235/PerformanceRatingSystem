namespace PerformanceRatingSystem.Application.Dtos;

public class AchievementForUpdateDto 
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public DateOnly DateAchieved { get; set; }
	public Guid EmployeeId { get; set; }
}

