using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerformanceRatingSystem.Domain.Entities;

public class DepartmentPerformanceIndicator
{
    [Key]
    public Guid IndicatorId { get; set; }

    public string Name { get; set; } = null!;

    public Guid DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<PlannedPerformanceValue> PlannedPerformanceValues { get; set; } = new List<PlannedPerformanceValue>();
}
