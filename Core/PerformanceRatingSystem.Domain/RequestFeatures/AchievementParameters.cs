namespace PerformanceRatingSystem.Domain.RequestFeatures;

public class AchievementParameters : RequestParameters
{
    public string? SearchDescription { get; set; }
    public AchievementParameters()
    {
        OrderBy = "dateAchieved";
    }
}
