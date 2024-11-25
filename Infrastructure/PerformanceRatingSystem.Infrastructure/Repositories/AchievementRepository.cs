using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Domain.RequestFeatures;
using PerformanceRatingSystem.Infrastructure.Extensions;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class AchievementRepository(EmployeePerformanceContext dbContext) : IAchievementRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(Achievement entity) => await _dbContext.Achievements.AddAsync(entity);

    public async Task<PagedList<Achievement>> Get(AchievementParameters achievementParameters, bool trackChanges)
    {
        IQueryable<Achievement> query = _dbContext.Achievements.Include(e => e.Employee);

        if (!trackChanges)
            query = query.AsNoTracking();

        query = query.SearchByDescription(achievementParameters.SearchDescription);

        var count = await query.CountAsync();

        var achievements = await query
            .Sort(achievementParameters.OrderBy)
            .Skip((achievementParameters.PageNumber - 1) * achievementParameters.PageSize)
            .Take(achievementParameters.PageSize)
            .ToListAsync();

        return new PagedList<Achievement>(
            achievements,
            count,
            achievementParameters.PageNumber,
            achievementParameters.PageSize
        );
    }

    public async Task<Achievement?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Achievements.Include(e => e.Employee).AsNoTracking() :
            _dbContext.Achievements.Include(e => e.Employee)).SingleOrDefaultAsync(e => e.AchievementId == id);

    public void Delete(Achievement entity) => _dbContext.Achievements.Remove(entity);

    public void Update(Achievement entity) => _dbContext.Achievements.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

