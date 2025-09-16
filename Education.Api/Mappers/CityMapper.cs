using Education.Api.Dtos;
using Education.Api.Models;

namespace Education.Api.Mappers;

public static class CityMapper
{
    public static Dtos.City ToDto(this Models.City city)
    {
        return new()
        {
            Id = city.Id,
            Name = city.Name,
            CreatedDate = city.CreatedDate,
            LastUpdatedDate = city.LastUpdatedDate

        };
    }

    public static Models.City ToModel(this Dtos.City city)
    {
        return new()
        {
            Id = city.Id,
            Name = city.Name,
            CreatedDate = city.CreatedDate,
            LastUpdatedDate = city.LastUpdatedDate
        };
    }

    public static Models.City ToModel(this Dtos.CreateUpdateCity citydto)
    {
        return new()
        {
            Name = citydto.Name
        };
    }
}
