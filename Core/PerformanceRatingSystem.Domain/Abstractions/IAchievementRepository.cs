using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Domain.Abstractions;

public interface IAchievementRepository 
{
	Task<IEnumerable<Achievement>> Get(bool trackChanges);
	Task<Achievement?> GetById(Guid id, bool trackChanges);
    Task Create(Achievement entity);
    void Delete(Achievement entity);
    void Update(Achievement entity);
    Task SaveChanges();
}

