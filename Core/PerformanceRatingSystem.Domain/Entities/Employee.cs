using System;
using System.Collections.Generic;

namespace PerformanceRatingSystem.Domain.Entities;

public class Employee
{
    public Guid EmployeeId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Midname { get; set; }

    public string Position { get; set; } = null!;

    public Guid DepartmentId { get; set; }

    public virtual ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<EmployeePerformanceIndicator> EmployeePerformanceIndicators { get; set; } = new List<EmployeePerformanceIndicator>();
}
