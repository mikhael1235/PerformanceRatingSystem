using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Domain.RequestFeatures;

namespace PerformanceRatingSystem.Domain.Abstractions;

public interface IDepartmentRepository 
{
    Task<PagedList<Department>> Get(DepartmentParameters departmentParameters, bool trackChanges);
    Task<IEnumerable<Department>> GetAll(bool trackChanges);

    Task<Department?> GetById(Guid id, bool trackChanges);
    Task Create(Department entity);
    void Delete(Department entity);
    void Update(Department entity);
    Task SaveChanges();
}

