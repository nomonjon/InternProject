namespace Education.Api.Dtos;

public class StudentSubject
{
    public Guid Id { get; set; }
    public double Mark { get; set; }
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateUpdateStudentSubject
{
    public double Mark { get; set; }
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
}