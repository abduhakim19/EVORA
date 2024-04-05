using API.Models;

namespace API.DTOs.Locations;

public class LocationDto
{

    public Guid Guid { get; set; }
    public string Street { get; set; }
    public string District { get; set; }
    public string SubDistrict { get; set; }
    public Guid? CityGuid { get; set; }
    public static explicit operator LocationDto(Location location)
    {
        return new LocationDto
        {
            Guid = location.Guid,
            Street = location.Street,
            District = location.District,
            SubDistrict = location.SubDistrict,
            CityGuid = location.CityGuid
        };
    }

    public static implicit operator Location(LocationDto locationDto)
    {
        return new Location
        {
            Guid = locationDto.Guid,
            Street = locationDto.Street,
            District = locationDto.District,
            SubDistrict = locationDto.SubDistrict,
            CityGuid = locationDto.CityGuid
        };
    }

}