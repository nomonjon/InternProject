namespace Education.Api.Models;

public class Teacher
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Guid CityId { get; set; }
    public City? City { get; set; }

    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }

    public List<TeacherSubject> TeacherSubjects { get; set; } = [];
}