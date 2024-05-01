using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Dtos;

public class VehicleSummaryDto
{
    public VehicleSummaryDto(Vehicle vehicle)
    {
        Id = vehicle.Id;
        Name = vehicle.Name;
        Slug = vehicle.Slug;
        Model = vehicle.Model;
        Manufacturer = vehicle.Manufacturer;
    }
    
    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Slug { get; } = string.Empty;
    public string Model { get; } = string.Empty;
    public string Manufacturer { get; } = string.Empty;
}
