using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Domain.RequestFeatures;
using PerformanceRatingSystem.Infrastructure.Extensions;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class DepartmentPerformanceIndicatorRepository(EmployeePerformanceContext dbContext) : IDepartmentPerformanceIndicatorRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(DepartmentPerformanceIndicator entity) => await _dbContext.DepartmentPerformanceIndicators.AddAsync(entity);

    public async Task<PagedList<DepartmentPerformanceIndicator>> Get(DepartmentPerformanceIndicatorParameters departmentPerformanceIndicatorParameters, bool trackChanges)
    {
        IQueryable<DepartmentPerformanceIndicator> query = _dbContext.DepartmentPerformanceIndicators.Include(x => x.Department);

        if (!trackChanges)
            query = query.AsNoTracking();

        query = query.SearchByName(departmentPerformanceIndicatorParameters.SearchName);

        var count = await query.CountAsync();

        var departmentPerformanceIndicators = await query
            .Sort(departmentPerformanceIndicatorParameters.OrderBy)
            .Skip((departmentPerformanceIndicatorParameters.PageNumber - 1) * departmentPerformanceIndicatorParameters.PageSize)
            .Take(departmentPerformanceIndicatorParameters.PageSize)
            .ToListAsync();

        return new PagedList<DepartmentPerformanceIndicator>(
            departmentPerformanceIndicators,
            count,
            departmentPerformanceIndicatorParameters.PageNumber,
            departmentPerformanceIndicatorParameters.PageSize
        );
    }

    public async Task<DepartmentPerformanceIndicator?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.DepartmentPerformanceIndicators.Include(e => e.Department).AsNoTracking() :
            _dbContext.DepartmentPerformanceIndicators.Include(e => e.Department)).SingleOrDefaultAsync(e => e.IndicatorId == id);

    public void Delete(DepartmentPerformanceIndicator entity) => _dbContext.DepartmentPerformanceIndicators.Remove(entity);

    public void Update(DepartmentPerformanceIndicator entity) => _dbContext.DepartmentPerformanceIndicators.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

