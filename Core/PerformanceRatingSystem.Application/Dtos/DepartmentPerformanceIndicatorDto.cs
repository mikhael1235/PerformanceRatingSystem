using System.ComponentModel;

namespace PerformanceRatingSystem.Application.Dtos;

public class DepartmentPerformanceIndicatorDto 
{
	public Guid IndicatorId { get; set; }
    [DisplayName("Название")]
    public string Name { get; set; }
	public Guid DepartmentId { get; set; }
    [DisplayName("Отдел")]
    public DepartmentDto Department { get; set; }
}

