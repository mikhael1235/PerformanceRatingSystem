using System.ComponentModel;

namespace PerformanceRatingSystem.Application.Dtos;

public class EmployeeDto 
{
	public Guid EmployeeId { get; set; }
    [DisplayName("ФИО")]
    public string FullName { get; set; }
    [DisplayName("Должность")]
    public string Position { get; set; }
	public Guid DepartmentId { get; set; }
    [DisplayName("Отдел")]
    public DepartmentDto Department { get; set; }
}

