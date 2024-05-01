using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Dtos;

public class VehicleDetailsDto
{
    public VehicleDetailsDto(Vehicle vehicle)
    {
        Id = vehicle.Id;
        Name = vehicle.Name;
        Slug = vehicle.Slug;
        Model = vehicle.Model;
        VehicleClass = vehicle.VehicleClass;
        Manufacturer = vehicle.Manufacturer;
        Length = vehicle.Length;
        CostInCredits = vehicle.CostInCredits;
        Crew = vehicle.Crew;
        Passengers = vehicle.Passengers;
        MaxAtmospheringSpeed = vehicle.MaxAtmospheringSpeed;
        CargoCapacity = vehicle.CargoCapacity;
        Consumables = vehicle.Consumables;
        Created = vehicle.Created;
        Edited = vehicle.Edited;
        Films = vehicle.Films
            .Select(film => new FilmSummaryDto(film)).ToList();
        Pilots = vehicle.Pilots
            .Select(pilot => new PersonSummaryDto(pilot)).ToList();
    }
    
    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Slug { get; } = string.Empty;
    public string Model { get; } = string.Empty;
    public string VehicleClass { get; } = string.Empty;
    public string Manufacturer { get; } = string.Empty;
    public int Length { get; }
    public decimal CostInCredits { get; }
    public string Crew { get; } = string.Empty;
    public int Passengers { get; }
    public int MaxAtmospheringSpeed { get; }
    public int CargoCapacity { get; }
    public string Consumables { get; } = string.Empty;
    public DateTime Created { get; }
    public DateTime Edited { get; }


    public List<FilmSummaryDto> Films { get; } = [];
    public List<PersonSummaryDto> Pilots { get; } = [];
}
