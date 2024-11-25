using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Domain.Abstractions;

public interface IDepartmentPerformanceIndicatorRepository 
{
    Task<PagedList<DepartmentPerformanceIndicator>> Get(DepartmentPerformanceIndicatorParameters departmentPerformanceIndicatorParameters, bool trackChanges);

    Task<DepartmentPerformanceIndicator?> GetById(Guid id, bool trackChanges);
    Task Create(DepartmentPerformanceIndicator entity);
    void Delete(DepartmentPerformanceIndicator entity);
    void Update(DepartmentPerformanceIndicator entity);
    Task SaveChanges();
}

