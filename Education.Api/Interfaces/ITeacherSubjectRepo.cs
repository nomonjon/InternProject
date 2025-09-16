using Education.Api.Models;

namespace Education.Api.Interfaces;

public interface ITeacherSubjectRepo
{
    Task<IEnumerable<Dtos.TeacherSubject>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Dtos.TeacherSubject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TeacherSubject> AddAsync(Dtos.CreateUpdateTeacherSubject teacherSubject, CancellationToken cancellationToken = default);
    Task<TeacherSubject?> UpdateAsync(Guid id, Dtos.CreateUpdateTeacherSubject teacherSubject, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
