using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using System.Linq;
using PerformanceRatingSystem.Domain.RequestFeatures;
using PerformanceRatingSystem.Infrastructure.Extensions;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class ActualPerformanceResultRepository(EmployeePerformanceContext dbContext) : IActualPerformanceResultRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(ActualPerformanceResult entity) => await _dbContext.ActualPerformanceResults.AddAsync(entity);

    public async Task<IEnumerable<ActualPerformanceResult>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.ActualPerformanceResults.Include(e => e.Indicator).AsNoTracking() 
            : _dbContext.ActualPerformanceResults.Include(e => e.Indicator)).ToListAsync();

    public async Task<PagedList<ActualPerformanceResult>> GetActualPerformanceResultsByDepartmentAsync(
        ActualPerformanceResultParameters productParameters,
        bool trackChanges)
    {
        var results =
            await (!trackChanges
            ? _dbContext.ActualPerformanceResults.Include(e => e.Indicator.Employee).Include(e => e.Indicator.Employee.Department).AsNoTracking()
            : _dbContext.ActualPerformanceResults.Include(e => e.Indicator.Employee).Include(e => e.Indicator.Employee.Department))
                .Search(productParameters.SearchQuarter, productParameters.SearchYear, productParameters.SearchDepartment)
                .Sort(productParameters.OrderBy)
                .ToListAsync();

        var count = await (!trackChanges
            ? _dbContext.ActualPerformanceResults.Include(e => e.Indicator).AsNoTracking()
            : _dbContext.ActualPerformanceResults.Include(e => e.Indicator)).CountAsync();

        return new PagedList<ActualPerformanceResult>(
            results,
            count,
            productParameters.PageNumber,
            productParameters.PageSize
        );
    }

    public async Task<ActualPerformanceResult?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.ActualPerformanceResults.Include(e => e.Indicator).AsNoTracking() 
            : _dbContext.ActualPerformanceResults.Include(e => e.Indicator))
                .SingleOrDefaultAsync(e => e.ResultId == id);

    public void Delete(ActualPerformanceResult entity) => _dbContext.ActualPerformanceResults.Remove(entity);

    public void Update(ActualPerformanceResult entity) => _dbContext.ActualPerformanceResults.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

