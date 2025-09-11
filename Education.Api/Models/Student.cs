namespace Education.Api.Models;

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Guid CityId { get; set; }
    public City? City { get; set; }


    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public int CurrentGradeLevel { get; set; }

    public Guid DepartmentId { get; set; }
    public Department? Department { get; set; }


    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }

    public List<StudentSubject> StudentSubjects { get; set; } = [];

}