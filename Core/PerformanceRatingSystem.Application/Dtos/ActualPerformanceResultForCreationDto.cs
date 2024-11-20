namespace PerformanceRatingSystem.Application.Dtos;

public class ActualPerformanceResultForCreationDto 
{
	public string IndicatorType { get; set; }
	public decimal Value { get; set; }
	public int Year { get; set; }
	public int Quarter { get; set; }
	public Guid IndicatorId { get; set; }
}
