using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class DepartmentPerformanceIndicatorRepository(EmployeePerformanceContext dbContext) : IDepartmentPerformanceIndicatorRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(DepartmentPerformanceIndicator entity) => await _dbContext.DepartmentPerformanceIndicators.AddAsync(entity);

    public async Task<IEnumerable<DepartmentPerformanceIndicator>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.DepartmentPerformanceIndicators.Include(e => e.Department).AsNoTracking() 
            : _dbContext.DepartmentPerformanceIndicators.Include(e => e.Department)).ToListAsync();

    public async Task<DepartmentPerformanceIndicator?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.DepartmentPerformanceIndicators.Include(e => e.Department).AsNoTracking() :
            _dbContext.DepartmentPerformanceIndicators.Include(e => e.Department)).SingleOrDefaultAsync(e => e.IndicatorId == id);

    public void Delete(DepartmentPerformanceIndicator entity) => _dbContext.DepartmentPerformanceIndicators.Remove(entity);

    public void Update(DepartmentPerformanceIndicator entity) => _dbContext.DepartmentPerformanceIndicators.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

