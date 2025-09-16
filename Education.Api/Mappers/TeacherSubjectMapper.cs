namespace Education.Api.Mappers;

public static class TeacherSubjectMapper
{
    public static Models.TeacherSubject ToModel(this Dtos.CreateUpdateTeacherSubject teacherSubject)
    {
        return new()
        {
            TeacherId = teacherSubject.TeacherId,
            SubjectId = teacherSubject.SubjectId
        };
    }

    public static Dtos.TeacherSubject ToDto(this Models.TeacherSubject teacherSubject)
    {
        return new()
        {
            Id = teacherSubject.Id,
            TeacherId = teacherSubject.TeacherId,
            SubjectId = teacherSubject.SubjectId,
            CreatedDate = teacherSubject.CreatedDate,
            LastUpdatedDate = teacherSubject.LastUpdatedDate,
            IsDeleted = teacherSubject.IsDeleted
        };
    }
}