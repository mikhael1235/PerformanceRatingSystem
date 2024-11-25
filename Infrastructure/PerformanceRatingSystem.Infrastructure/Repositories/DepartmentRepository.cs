using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;
using PerformanceRatingSystem.Domain.RequestFeatures;
using PerformanceRatingSystem.Infrastructure.Extensions;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class DepartmentRepository(EmployeePerformanceContext dbContext) : IDepartmentRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(Department entity) => await _dbContext.Departments.AddAsync(entity);

    public async Task<PagedList<Department>> Get(DepartmentParameters departmentParameters, bool trackChanges)
    {
        IQueryable<Department> query = _dbContext.Departments;

        if (!trackChanges)
            query = query.AsNoTracking();

        query = query.SearchByName(departmentParameters.SearchName);

        var count = await query.CountAsync();

        var departments = await query
            .Sort(departmentParameters.OrderBy)
            .Skip((departmentParameters.PageNumber - 1) * departmentParameters.PageSize)
            .Take(departmentParameters.PageSize)
            .ToListAsync();

        return new PagedList<Department>(
            departments,
            count,
            departmentParameters.PageNumber,
            departmentParameters.PageSize
        );
    }

    public async Task<Department?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Departments.Include(x => x.Employees).AsNoTracking() :
            _dbContext.Departments.Include(x => x.Employees)).SingleOrDefaultAsync(e => e.DepartmentId == id);

    public void Delete(Department entity) => _dbContext.Departments.Remove(entity);

    public void Update(Department entity) => _dbContext.Departments.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

