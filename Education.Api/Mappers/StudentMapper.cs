namespace Education.Api.Mappers;

public static class StudentMapper
{
    public static Models.Student ToModel(this Dtos.CreateUpdateStudent student)
    {
        return new()
        {
            Name = student.Name,
            BirthDate = student.BirthDate,
            Gender = student.Gender switch
            {
                Dtos.GenderDto.Male => Models.Gender.Male,
                Dtos.GenderDto.Female => Models.Gender.Female,
                _ => Models.Gender.Unknown
            },
            CityId = student.CityId,
            DepartmentId = student.DepartmentId,
            CurrentGradeLevel = student.CurrentGradeLevel
        };
    }
    public static Dtos.Student ToDto(this Models.Student student)
    {
        return new()
        {
            Id = student.Id,
            Name = student.Name,
            BirthDate = student.BirthDate,
            CityName = student.City?.Name ?? "Unknown",
            DepartmentName = student.Department?.Name ?? "Unknown",
            Gender = student.Gender switch
            {
                Models.Gender.Male => Dtos.GenderDto.Male,
                Models.Gender.Female => Dtos.GenderDto.Female,
                _ => Dtos.GenderDto.Unknown
            },
            CurrentGradeLevel = student.CurrentGradeLevel,
            CreatedDate = student.CreatedDate,
            LastUpdatedDate = student.LastUpdatedDate,
            IsDeleted = student.IsDeleted
        };
    }

}
