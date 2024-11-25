namespace PerformanceRatingSystem.Domain.RequestFeatures;

public class EmployeeParameters : RequestParameters
{
    public string? SearchPosition { get; set; }
    public EmployeeParameters()
    {
        OrderBy = "surname";
    }
}
