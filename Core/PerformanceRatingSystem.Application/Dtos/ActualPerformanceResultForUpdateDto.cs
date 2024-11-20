namespace PerformanceRatingSystem.Application.Dtos;

public class ActualPerformanceResultForUpdateDto 
{
	public Guid Id { get; set; }
	public string IndicatorType { get; set; }
	public decimal Value { get; set; }
	public int Year { get; set; }
	public int Quarter { get; set; }
	public Guid IndicatorId { get; set; }
}

