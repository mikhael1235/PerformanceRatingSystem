using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerformanceRatingSystem.Domain.Entities;

public class PlannedPerformanceValue
{
    [Key]
    public Guid PlanId { get; set; }

    public string IndicatorType { get; set; } = null!;

    public decimal Value { get; set; }

    public short Year { get; set; }

    public byte Quarter { get; set; }

    public Guid IndicatorId { get; set; }

    public virtual DepartmentPerformanceIndicator Indicator { get; set; } = null!;
}
