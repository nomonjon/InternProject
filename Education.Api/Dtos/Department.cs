namespace Education.Api.Dtos;

public class Department
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateUpdateDepartment
{
        public string Name { get; set; } = string.Empty;
}