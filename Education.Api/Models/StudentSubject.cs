namespace Education.Api.Models;

public class StudentSubject
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }
    public Student? Student { get; set; }

    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    public double Mark { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}