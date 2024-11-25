using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using System.Linq;
using PerformanceRatingSystem.Domain.RequestFeatures;
using PerformanceRatingSystem.Infrastructure.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class ActualPerformanceResultRepository(EmployeePerformanceContext dbContext) : IActualPerformanceResultRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(ActualPerformanceResult entity) => await _dbContext.ActualPerformanceResults.AddAsync(entity);

    public async Task<PagedList<ActualPerformanceResult>> Get(
    ActualPerformanceResultParameters productParameters,
    bool trackChanges)
    {
        IQueryable<ActualPerformanceResult> query = _dbContext.ActualPerformanceResults
            .Include(e => e.Indicator.Employee)
            .Include(e => e.Indicator.Employee.Department);

        if (!trackChanges)
            query = query.AsNoTracking();

        query = query
            .Search(productParameters.SearchQuarter, productParameters.SearchYear, productParameters.SearchDepartment) // Предполагаю, что здесь ваш метод поиска
            .Sort(productParameters.OrderBy); 
        var count = await query.CountAsync();

        var results = await query
            .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
            .Take(productParameters.PageSize)
            .ToListAsync();

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

