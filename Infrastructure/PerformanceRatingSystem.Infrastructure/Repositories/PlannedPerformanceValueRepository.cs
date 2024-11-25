using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Domain.RequestFeatures;
using PerformanceRatingSystem.Infrastructure.Extensions;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class PlannedPerformanceValueRepository(EmployeePerformanceContext dbContext) : IPlannedPerformanceValueRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(PlannedPerformanceValue entity) => await _dbContext.PlannedPerformanceValues.AddAsync(entity);

    public async Task<PagedList<PlannedPerformanceValue>> Get(
        PlannedPerformanceValueParameters productParameters,
        bool trackChanges)
    {
        IQueryable<PlannedPerformanceValue> query = _dbContext.PlannedPerformanceValues.Include(e => e.Indicator.Department);

        if (!trackChanges)
            query = query.AsNoTracking();

        var newquery = query
               .Search(productParameters.SearchQuarter, productParameters.SearchYear);


        var results =
            await newquery
                .Sort(productParameters.OrderBy)
                .ToListAsync();

        var count = await newquery.CountAsync();
        return new PagedList<PlannedPerformanceValue>(
            results,
            count,
            productParameters.PageNumber,
            productParameters.PageSize
      );
    }

    public async Task<PlannedPerformanceValue?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.PlannedPerformanceValues.Include(e => e.Indicator).AsNoTracking() :
            _dbContext.PlannedPerformanceValues.Include(e => e.Indicator)).SingleOrDefaultAsync(e => e.PlanId == id);

    public void Delete(PlannedPerformanceValue entity) => _dbContext.PlannedPerformanceValues.Remove(entity);

    public void Update(PlannedPerformanceValue entity) => _dbContext.PlannedPerformanceValues.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

