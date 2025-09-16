namespace Education.Api.Dtos;

public class Teacher
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public GenderDto Gender { get; set; }
    public string CityName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateUpdateTeacher
{
    public string Name { get; set; } = string.Empty;
    public Guid CityId { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderDto Gender { get; set; }
}