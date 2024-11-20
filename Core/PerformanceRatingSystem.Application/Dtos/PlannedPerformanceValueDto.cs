using System.ComponentModel;

namespace PerformanceRatingSystem.Application.Dtos;

public class PlannedPerformanceValueDto 
{
	public Guid PlanId { get; set; }
    [DisplayName("Тип показателя")]
    public string? IndicatorType { get; set; }

    [DisplayName("Результат")]
    public decimal Value { get; set; }

    [DisplayName("Год")]
    public short Year { get; set; }

    [DisplayName("Квартал")]
    public byte Quarter { get; set; }

    [DisplayName("Показатель")]
	public DepartmentPerformanceIndicatorDto Indicator { get; set; }
    public Guid IndicatorId { get; set; }
}

