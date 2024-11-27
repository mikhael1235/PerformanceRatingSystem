using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Domain.Abstractions;

public interface IPlannedPerformanceValueRepository 
{
    Task<PagedList<PlannedPerformanceValue>> Get(
        PlannedPerformanceValueParameters productParameters,
        bool trackChanges);

    Task<PlannedPerformanceValue?> GetById(Guid id, bool trackChanges);
    Task Create(PlannedPerformanceValue entity);
    void Delete(PlannedPerformanceValue entity);
    void Update(PlannedPerformanceValue entity);
    Task SaveChanges();
}

