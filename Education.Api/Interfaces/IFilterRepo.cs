namespace Education.Api.Dtos;

public interface IFilterRepo
{
    Task<IEnumerable<Student>> FilterStudentsAsync(StudentFilterDto filter, CancellationToken cancellationToken = default);
    Task<IEnumerable<Teacher>> FilterTeachersAsync(TeacherFilterDto filter, CancellationToken cancellationToken = default);
    Task<IEnumerable<Student>> GetTop10StudentsBySubjectAsync(Guid subjectId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Teacher>> GetTop10TeachersByTopStudentsAsync(Guid subjectId, bool best, CancellationToken cancellationToken = default);
}
