using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Domain.Abstractions;

public interface IPlannedPerformanceValueRepository 
{
	Task<IEnumerable<PlannedPerformanceValue>> Get(bool trackChanges);
	Task<PlannedPerformanceValue?> GetById(Guid id, bool trackChanges);
    Task Create(PlannedPerformanceValue entity);
    void Delete(PlannedPerformanceValue entity);
    void Update(PlannedPerformanceValue entity);
    Task SaveChanges();
}

