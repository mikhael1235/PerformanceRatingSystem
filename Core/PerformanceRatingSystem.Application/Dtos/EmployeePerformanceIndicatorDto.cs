using System.ComponentModel;

namespace PerformanceRatingSystem.Application.Dtos;

public class EmployeePerformanceIndicatorDto 
{
	public Guid IndicatorId { get; set; }
    [DisplayName("Название")]
    public string Name { get; set; }
	public Guid EmployeeId { get; set; }
    [DisplayName("Сотрудник")]
    public EmployeeDto Employee { get; set; }
}

