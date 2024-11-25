namespace PerformanceRatingSystem.Domain.RequestFeatures;

public class AchievementParameters : RequestParameters
{
    public string? Description { get; set; }
    public AchievementParameters()
    {
        OrderBy = "dateAchieved";
    }
}
