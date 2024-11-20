using System;
using System.Collections.Generic;

namespace PerformanceRatingSystem.Domain.Entities;

public class Achievement
{
    public Guid AchievementId { get; set; }

    public Guid EmployeeId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly? DateAchieved { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
