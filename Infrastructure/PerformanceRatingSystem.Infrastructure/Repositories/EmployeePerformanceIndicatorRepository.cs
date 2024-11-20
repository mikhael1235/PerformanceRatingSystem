using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class EmployeePerformanceIndicatorRepository(EmployeePerformanceContext dbContext) : IEmployeePerformanceIndicatorRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(EmployeePerformanceIndicator entity) => await _dbContext.EmployeePerformanceIndicators.AddAsync(entity);

    public async Task<IEnumerable<EmployeePerformanceIndicator>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.EmployeePerformanceIndicators.Include(e => e.Employee).AsNoTracking() 
            : _dbContext.EmployeePerformanceIndicators.Include(e => e.Employee)).ToListAsync();

    public async Task<EmployeePerformanceIndicator?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.EmployeePerformanceIndicators.Include(e => e.Employee).AsNoTracking() :
            _dbContext.EmployeePerformanceIndicators.Include(e => e.Employee)).SingleOrDefaultAsync(e => e.IndicatorId == id);

    public void Delete(EmployeePerformanceIndicator entity) => _dbContext.EmployeePerformanceIndicators.Remove(entity);

    public void Update(EmployeePerformanceIndicator entity) => _dbContext.EmployeePerformanceIndicators.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

