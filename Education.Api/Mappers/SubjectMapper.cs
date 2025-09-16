namespace Education.Api.Mappers;

public static class SubjectMapper
{
    public static Models.Subject ToModel(this Dtos.CreateUpdateSubject subject)
    {
        return new()
        {
            Name = subject.Name,
            GradeLevel = subject.GradeLevel
        };
    }
}
