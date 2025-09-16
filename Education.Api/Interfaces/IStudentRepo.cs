using Education.Api.Models;

namespace Education.Api.Interfaces;

public interface IStudentRepo
{
    Task<IEnumerable<Dtos.Student>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Dtos.Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Student> AddAsync(Dtos.CreateUpdateStudent student, CancellationToken cancellationToken = default);
    Task<Student?> UpdateAsync(Guid id, Dtos.CreateUpdateStudent student, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
