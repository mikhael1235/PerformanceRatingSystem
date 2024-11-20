using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Domain.Abstractions;

public interface IEmployeePerformanceIndicatorRepository 
{
	Task<IEnumerable<EmployeePerformanceIndicator>> Get(bool trackChanges);
	Task<EmployeePerformanceIndicator?> GetById(Guid id, bool trackChanges);
    Task Create(EmployeePerformanceIndicator entity);
    void Delete(EmployeePerformanceIndicator entity);
    void Update(EmployeePerformanceIndicator entity);
    Task SaveChanges();
}

