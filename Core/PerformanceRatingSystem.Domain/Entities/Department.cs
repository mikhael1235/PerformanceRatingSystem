using System;
using System.Collections.Generic;

namespace PerformanceRatingSystem.Domain.Entities;

public class Department
{
    public Guid DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DepartmentPerformanceIndicator> DepartmentPerformanceIndicators { get; set; } = new List<DepartmentPerformanceIndicator>();

    public virtual ICollection<Employee> Employees { get; set; } = [];
}
