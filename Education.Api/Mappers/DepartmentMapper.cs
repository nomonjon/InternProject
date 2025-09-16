namespace Education.Api.Mappers;

public static class DepartmentMapper
{
    public static Models.Department ToModel(this Dtos.CreateUpdateDepartment department)
    {
        return new()
        {
            Name = department.Name
        };
    }
}
