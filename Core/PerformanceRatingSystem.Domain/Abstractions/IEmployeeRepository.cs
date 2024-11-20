using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Domain.Abstractions;

public interface IEmployeeRepository 
{
	Task<IEnumerable<Employee>> Get(bool trackChanges);
	Task<Employee?> GetById(Guid id, bool trackChanges);
    Task Create(Employee entity);
    void Delete(Employee entity);
    void Update(Employee entity);
    Task SaveChanges();
}

