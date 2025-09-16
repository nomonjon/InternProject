namespace Education.Api.Mappers;

public static class StudentSubjectMapper
{
    public static Models.StudentSubject ToModel(this Dtos.CreateUpdateStudentSubject studentSubject)
    {
        return new()
        {
            Mark = studentSubject.Mark,
            StudentId = studentSubject.StudentId,
            SubjectId = studentSubject.SubjectId
        };
    }
    public static Dtos.StudentSubject ToDto(this Models.StudentSubject studentSubject)
    {
        return new()
        {
            Id = studentSubject.Id,
            StudentId = studentSubject.StudentId,
            SubjectId = studentSubject.SubjectId,
            Mark = studentSubject.Mark,
            CreatedDate = studentSubject.CreatedDate,
            LastUpdatedDate = studentSubject.LastUpdatedDate,
            IsDeleted = studentSubject.IsDeleted
        };
    }
}
