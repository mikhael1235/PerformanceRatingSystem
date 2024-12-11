using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Domain.RequestFeatures;
using PerformanceRatingSystem.Infrastructure.Extensions;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class EmployeeRepository(EmployeePerformanceContext dbContext) : IEmployeeRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(Employee entity) => await _dbContext.Employees.AddAsync(entity);

    public async Task<PagedList<Employee>> Get(EmployeeParameters employeeParameters, bool trackChanges)
    {
        IQueryable<Employee> query = _dbContext.Employees;

        if (!trackChanges)
            query = query.AsNoTracking();

        query = query.SearchByPosition(employeeParameters.SearchPosition);

        var count = await query.CountAsync();

        var employees = await query
            .Sort(employeeParameters.OrderBy)
            .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            .Take(employeeParameters.PageSize)
            .ToListAsync();

        return new PagedList<Employee>(
            employees,
            count,
            employeeParameters.PageNumber,
            employeeParameters.PageSize
        );
    }

    public async Task<Employee?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Employees.AsNoTracking() :
            _dbContext.Employees).SingleOrDefaultAsync(e => e.EmployeeId == id);

    public void Delete(Employee entity) => _dbContext.Employees.Remove(entity);

    public void Update(Employee entity) => _dbContext.Employees.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

