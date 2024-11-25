using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Domain.Abstractions;

public interface IAchievementRepository 
{
    Task<PagedList<Achievement>> Get(AchievementParameters achievementParameters, bool trackChanges);
    Task<Achievement?> GetById(Guid id, bool trackChanges);
    Task Create(Achievement entity);
    void Delete(Achievement entity);
    void Update(Achievement entity);
    Task SaveChanges();
}

