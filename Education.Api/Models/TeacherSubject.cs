namespace Education.Api.Models;

public class TeacherSubject
{
    public Guid Id { get; set; }

    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}