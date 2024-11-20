using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Domain.Abstractions;

public interface IDepartmentPerformanceIndicatorRepository 
{
	Task<IEnumerable<DepartmentPerformanceIndicator>> Get(bool trackChanges);
	Task<DepartmentPerformanceIndicator?> GetById(Guid id, bool trackChanges);
    Task Create(DepartmentPerformanceIndicator entity);
    void Delete(DepartmentPerformanceIndicator entity);
    void Update(DepartmentPerformanceIndicator entity);
    Task SaveChanges();
}

