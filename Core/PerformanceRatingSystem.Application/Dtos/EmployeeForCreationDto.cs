namespace PerformanceRatingSystem.Application.Dtos;

public class EmployeeForCreationDto 
{
	public string Surname { get; set; }
	public string Name { get; set; }
	public string Midname { get; set; }
	public string Position { get; set; }
	public Guid DepartmentId { get; set; }
}

