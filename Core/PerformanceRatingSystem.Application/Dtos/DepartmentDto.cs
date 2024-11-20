using System.ComponentModel;
using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Application.Dtos;

public class DepartmentDto 
{
	public Guid DepartmentId { get; set; }
    [DisplayName("Название")]
    public string Name { get; set; }

    [DisplayName("Сотрудники")]
    public ICollection<EmployeeDto> Employees { get; set; }
}

