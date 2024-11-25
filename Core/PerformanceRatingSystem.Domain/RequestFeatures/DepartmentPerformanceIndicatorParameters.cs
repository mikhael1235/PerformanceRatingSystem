namespace PerformanceRatingSystem.Domain.RequestFeatures;

public class DepartmentPerformanceIndicatorParameters : RequestParameters
{
    public string? SearchName { get; set; }
    public DepartmentPerformanceIndicatorParameters()
    {
        OrderBy = "name";
    }
}
