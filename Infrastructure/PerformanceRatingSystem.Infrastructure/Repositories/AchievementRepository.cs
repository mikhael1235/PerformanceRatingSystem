using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class AchievementRepository(EmployeePerformanceContext dbContext) : IAchievementRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(Achievement entity) => await _dbContext.Achievements.AddAsync(entity);

    public async Task<IEnumerable<Achievement>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Achievements.Include(e => e.Employee).AsNoTracking() 
            : _dbContext.Achievements.Include(e => e.Employee)).ToListAsync();

    public async Task<Achievement?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Achievements.Include(e => e.Employee).AsNoTracking() :
            _dbContext.Achievements.Include(e => e.Employee)).SingleOrDefaultAsync(e => e.AchievementId == id);

    public void Delete(Achievement entity) => _dbContext.Achievements.Remove(entity);

    public void Update(Achievement entity) => _dbContext.Achievements.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

