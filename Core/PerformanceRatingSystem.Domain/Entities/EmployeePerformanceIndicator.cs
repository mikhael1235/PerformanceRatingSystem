using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerformanceRatingSystem.Domain.Entities;

public class EmployeePerformanceIndicator
{
    [Key]
    public Guid IndicatorId { get; set; }

    public string Name { get; set; } = null!;

    public Guid EmployeeId { get; set; }

    public virtual ICollection<ActualPerformanceResult> ActualPerformanceResults { get; set; } = new List<ActualPerformanceResult>();

    public virtual Employee Employee { get; set; } = null!;
}
