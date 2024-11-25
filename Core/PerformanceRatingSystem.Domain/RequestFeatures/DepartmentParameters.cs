namespace PerformanceRatingSystem.Domain.RequestFeatures;

public class DepartmentParameters : RequestParameters
{
    public string? SearchName { get; set; }
    public DepartmentParameters()
    {
        OrderBy = "name";
    }
}
