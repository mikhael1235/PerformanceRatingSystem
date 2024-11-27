using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Domain.Abstractions;

public interface IActualPerformanceResultRepository 
{
    Task<PagedList<ActualPerformanceResult>> Get(
        ActualPerformanceResultParameters productParameters,
        bool trackChanges);

    Task<ActualPerformanceResult?> GetById(Guid id, bool trackChanges);
    Task Create(ActualPerformanceResult entity);
    void Delete(ActualPerformanceResult entity);
    void Update(ActualPerformanceResult entity);
    Task SaveChanges();
}

