using Education.Api.Models;

namespace Education.Api.Interfaces;

public interface ITeacherRepo
{
    Task<IEnumerable<Dtos.Teacher>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Dtos.Teacher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Teacher> AddAsync(Dtos.CreateUpdateTeacher teacher, CancellationToken cancellationToken = default);
    Task<Teacher?> UpdateAsync(Guid id, Dtos.CreateUpdateTeacher teacher, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
