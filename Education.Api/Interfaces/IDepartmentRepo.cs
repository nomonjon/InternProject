using Education.Api.Models;

namespace Education.Api.Interfaces;

public interface IDepartmentRepo
{
    Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Department> AddAsync(Dtos.CreateUpdateDepartment department, CancellationToken cancellationToken = default);
    Task<Department?> UpdateAsync(Guid id, Dtos.CreateUpdateDepartment department, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
