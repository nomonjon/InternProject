namespace Education.Api.Mappers;

public static class TeacherMapper
{
    public static Models.Teacher ToModel(this Dtos.CreateUpdateTeacher teacher)
    {
        return new()
        {
            Name = teacher.Name,
            BirthDate = teacher.BirthDate,
            Gender = teacher.Gender switch
            {
                Dtos.GenderDto.Male => Models.Gender.Male,
                Dtos.GenderDto.Female => Models.Gender.Female,
                _ => Models.Gender.Unknown
            },
            CityId = teacher.CityId
        };
    }

        public static Dtos.Teacher ToDto(this Models.Teacher teacher)
    {
        return new()
        {
            Id = teacher.Id,
            Name = teacher.Name,
            BirthDate = teacher.BirthDate,
            CityName = teacher.City?.Name ?? "Unknown",
            Gender = teacher.Gender switch
            {
                Models.Gender.Male => Dtos.GenderDto.Male,
                Models.Gender.Female => Dtos.GenderDto.Female,
                _ => Dtos.GenderDto.Unknown
            },
            CreatedDate = teacher.CreatedDate,
            LastUpdatedDate = teacher.LastUpdatedDate,
            IsDeleted = teacher.IsDeleted
        };
    }
}
