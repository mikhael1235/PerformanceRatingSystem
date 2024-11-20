using System.ComponentModel;

namespace PerformanceRatingSystem.Application.Dtos;

public class AchievementDto 
{
	public Guid AchievementId { get; set; }
    [DisplayName("Описание")]
    public string Description { get; set; }
    [DisplayName("Дата достижения")]
    public DateOnly DateAchieved { get; set; }
	public Guid EmployeeId { get; set; }
    [DisplayName("Сотрудник")]
    public EmployeeDto Employee { get; set; }
}

