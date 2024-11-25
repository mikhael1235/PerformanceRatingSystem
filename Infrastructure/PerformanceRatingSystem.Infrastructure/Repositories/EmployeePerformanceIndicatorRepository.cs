using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Domain.RequestFeatures;
using PerformanceRatingSystem.Infrastructure.Extensions;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class EmployeePerformanceIndicatorRepository(EmployeePerformanceContext dbContext) : IEmployeePerformanceIndicatorRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(EmployeePerformanceIndicator entity) => await _dbContext.EmployeePerformanceIndicators.AddAsync(entity);

    public async Task<PagedList<EmployeePerformanceIndicator>> Get(EmployeePerformanceIndicatorParameters employeePerformanceIndicatorParameters, bool trackChanges)
    {
        IQueryable<EmployeePerformanceIndicator> query = _dbContext.EmployeePerformanceIndicators.Include(x => x.Employee);

        if (!trackChanges)
            query = query.AsNoTracking();

        query = query.SearchByName(employeePerformanceIndicatorParameters.SearchName);

        var count = await query.CountAsync();

        var employeePerformanceIndicators = await query
            .Sort(employeePerformanceIndicatorParameters.OrderBy)
            .Skip((employeePerformanceIndicatorParameters.PageNumber - 1) * employeePerformanceIndicatorParameters.PageSize)
            .Take(employeePerformanceIndicatorParameters.PageSize)
            .ToListAsync();

        return new PagedList<EmployeePerformanceIndicator>(
            employeePerformanceIndicators,
            count,
            employeePerformanceIndicatorParameters.PageNumber,
            employeePerformanceIndicatorParameters.PageSize
        );
    }
    public async Task<EmployeePerformanceIndicator?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.EmployeePerformanceIndicators.Include(e => e.Employee).AsNoTracking() :
            _dbContext.EmployeePerformanceIndicators.Include(e => e.Employee)).SingleOrDefaultAsync(e => e.IndicatorId == id);

    public void Delete(EmployeePerformanceIndicator entity) => _dbContext.EmployeePerformanceIndicators.Remove(entity);

    public void Update(EmployeePerformanceIndicator entity) => _dbContext.EmployeePerformanceIndicators.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

