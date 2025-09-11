namespace Education.Api.Models;

public class City
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }

    public List<Student> Students { get; set; } = [];
    public List<Teacher> Teachers { get; set; } = [];
}
