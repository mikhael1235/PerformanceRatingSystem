using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.Abstractions;

namespace PerformanceRatingSystem.Infrastructure.Repositories;

public class DepartmentRepository(EmployeePerformanceContext dbContext) : IDepartmentRepository
{
    private readonly EmployeePerformanceContext _dbContext = dbContext;

    public async Task Create(Department entity) => await _dbContext.Departments.AddAsync(entity);

    public async Task<IEnumerable<Department>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Departments.Include(x => x.Employees).AsNoTracking() 
            : _dbContext.Departments.Include(x => x.Employees)).ToListAsync();

    public async Task<Department?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Departments.Include(x => x.Employees).AsNoTracking() :
            _dbContext.Departments.Include(x => x.Employees)).SingleOrDefaultAsync(e => e.DepartmentId == id);

    public void Delete(Department entity) => _dbContext.Departments.Remove(entity);

    public void Update(Department entity) => _dbContext.Departments.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

