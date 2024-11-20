using System.ComponentModel;

namespace PerformanceRatingSystem.Application.Dtos;

public class DepartmentWithResultDto
{
	public Guid DepartmentId { get; set; }
    [DisplayName("ФИО")]
    public string Name { get; set; }
    [DisplayName("Результат")]
    public decimal Value { get; set; }

    [DisplayName("Год")]
    public short Year { get; set; }

    [DisplayName("Квартал")]
    public byte Quarter { get; set; }
}

