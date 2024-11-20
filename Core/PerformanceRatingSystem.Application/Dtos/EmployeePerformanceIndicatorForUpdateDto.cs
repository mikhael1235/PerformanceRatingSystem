namespace PerformanceRatingSystem.Application.Dtos;

public class EmployeePerformanceIndicatorForUpdateDto 
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public Guid EmployeeId { get; set; }
}

