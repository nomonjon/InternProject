namespace Education.Api.Dtos;

public class StudentFilterDto
{
    public int? Gender { get; set; }
    public Guid? DepartmentId { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public Guid? CityId { get; set; }
    public int? Grade { get; set; }
    public List<Guid>? SubjectIds { get; set; }
}
public class TeacherFilterDto
{
    public int? Gender { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public List<Guid>? SubjectIds { get; set; }    // какие предметы ведёт
}
