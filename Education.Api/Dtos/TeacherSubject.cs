namespace Education.Api.Dtos;

public class TeacherSubject
{
    public Guid Id { get; set; }
    public Guid TeacherId { get; set; }
    public Guid SubjectId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateUpdateTeacherSubject
{
    public Guid TeacherId { get; set; }
    public Guid SubjectId { get; set; }
}
