using AutoMapper;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
		CreateMap<Department, DepartmentDto>();
		CreateMap<DepartmentForCreationDto, Department>();
		CreateMap<DepartmentForUpdateDto, Department>();

		CreateMap<Employee, EmployeeDto>().ForMember(e => e.FullName,
			opt => opt.MapFrom(x => string.Join(' ', x.Surname, x.Name, x.Midname)));
        CreateMap<EmployeeForCreationDto, Employee>();
		CreateMap<EmployeeForUpdateDto, Employee>();

		CreateMap<DepartmentPerformanceIndicator, DepartmentPerformanceIndicatorDto>();
		CreateMap<DepartmentPerformanceIndicatorForCreationDto, DepartmentPerformanceIndicator>();
		CreateMap<DepartmentPerformanceIndicatorForUpdateDto, DepartmentPerformanceIndicator>();

		CreateMap<PlannedPerformanceValue, PlannedPerformanceValueDto>();
		CreateMap<PlannedPerformanceValueForCreationDto, PlannedPerformanceValue>();
		CreateMap<PlannedPerformanceValueForUpdateDto, PlannedPerformanceValue>();

		CreateMap<EmployeePerformanceIndicator, EmployeePerformanceIndicatorDto>();
		CreateMap<EmployeePerformanceIndicatorForCreationDto, EmployeePerformanceIndicator>();
		CreateMap<EmployeePerformanceIndicatorForUpdateDto, EmployeePerformanceIndicator>();

		CreateMap<Achievement, AchievementDto>();
		CreateMap<AchievementForCreationDto, Achievement>();
		CreateMap<AchievementForUpdateDto, Achievement>();

		CreateMap<ActualPerformanceResult, ActualPerformanceResultDto>();
		CreateMap<ActualPerformanceResult, EmployeeWithResultDto>()
			.ForMember(x => x.FullName, opt =>
				opt.MapFrom(x =>
					string.Join(' ',
						x.Indicator.Employee.Surname,
						x.Indicator.Employee.Name,
						x.Indicator.Employee.Midname
					)
				)
			);
		CreateMap<ActualPerformanceResult, DepartmentWithResultDto>()
			.ForMember(x => x.Name, opt =>
				opt.MapFrom(x =>
					x.Indicator.Employee.Department.Name
				)
			);
            
        CreateMap<ActualPerformanceResultForCreationDto, ActualPerformanceResult>();
		CreateMap<ActualPerformanceResultForUpdateDto, ActualPerformanceResult>();
    }
}

