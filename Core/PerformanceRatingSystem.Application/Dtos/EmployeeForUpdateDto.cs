namespace PerformanceRatingSystem.Application.Dtos;

public class EmployeeForUpdateDto 
{
	public Guid Id { get; set; }
	public string Surname { get; set; }
	public string Name { get; set; }
	public string Midname { get; set; }
	public string Position { get; set; }
	public Guid DepartmentId { get; set; }
}

