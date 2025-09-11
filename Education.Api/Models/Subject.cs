namespace Education.Api.Models;

public class Subject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int GradeLevel { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }

    public List<TeacherSubject> TeacherSubjects { get; set; } = [];
    public List<StudentSubject> StudentSubjects { get; set; } = [];
}