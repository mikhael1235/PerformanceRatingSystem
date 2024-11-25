using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Domain.Abstractions;

public interface IEmployeePerformanceIndicatorRepository 
{
    Task<PagedList<EmployeePerformanceIndicator>> Get(EmployeePerformanceIndicatorParameters employeePerformanceIndicatorParameters, bool trackChanges);

    Task<EmployeePerformanceIndicator?> GetById(Guid id, bool trackChanges);
    Task Create(EmployeePerformanceIndicator entity);
    void Delete(EmployeePerformanceIndicator entity);
    void Update(EmployeePerformanceIndicator entity);
    Task SaveChanges();
}

