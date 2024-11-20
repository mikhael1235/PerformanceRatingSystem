using System.ComponentModel;

namespace PerformanceRatingSystem.Application.Dtos;

public class EmployeeWithResultDto
{
	public Guid EmployeeId { get; set; }
    [DisplayName("ФИО")]
    public string FullName { get; set; }
    [DisplayName("Результат")]
    public decimal Value { get; set; }

    [DisplayName("Год")]
    public short Year { get; set; }

    [DisplayName("Квартал")]
    public byte Quarter { get; set; }
}

