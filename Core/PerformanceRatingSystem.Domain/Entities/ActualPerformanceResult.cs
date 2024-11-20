using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerformanceRatingSystem.Domain.Entities;

public class ActualPerformanceResult
{
    [Key]
    public Guid ResultId { get; set; }

    public string? IndicatorType { get; set; }

    public decimal? Value { get; set; }

    public short Year { get; set; }

    public byte Quarter { get; set; }

    public Guid IndicatorId { get; set; }

    public virtual EmployeePerformanceIndicator Indicator { get; set; } = null!;
}
