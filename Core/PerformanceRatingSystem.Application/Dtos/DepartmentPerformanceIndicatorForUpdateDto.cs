namespace PerformanceRatingSystem.Application.Dtos;

public class DepartmentPerformanceIndicatorForUpdateDto 
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public Guid DepartmentId { get; set; }
}

