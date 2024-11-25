namespace PerformanceRatingSystem.Domain.RequestFeatures;

public class EmployeePerformanceIndicatorParameters : RequestParameters
{
    public string? SearchName { get; set; }
    public EmployeePerformanceIndicatorParameters()
    {
        OrderBy = "name";
    }
}
