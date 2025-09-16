using Education.Api.Models;

namespace Education.Api.Interfaces;

public interface IStudentSubjectRepo
{
    Task<IEnumerable<Dtos.StudentSubject>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Dtos.StudentSubject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<StudentSubject> AddAsync(Dtos.CreateUpdateStudentSubject studentSubject, CancellationToken cancellationToken = default);
    Task<StudentSubject?> UpdateAsync(Guid id, Dtos.CreateUpdateStudentSubject studentSubject, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
