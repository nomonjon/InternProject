namespace Education.Api.Dtos;

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CityName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public GenderDto Gender { get; set; }
    public int CurrentGradeLevel { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateUpdateStudent
{
    public string Name { get; set; } = string.Empty;
    public Guid CityId { get; set; }
    public Guid DepartmentId { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderDto Gender { get; set; }
    public int CurrentGradeLevel { get; set; }
}