using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Dtos;

public class StarshipDetailsDto
{
    public StarshipDetailsDto(Starship starship)
    {
        Id = starship.Id;
        Name = starship.Name;
        Slug = starship.Slug;
        Model = starship.Model;
        StarshipClass = starship.StarshipClass;
        Manufacturer = starship.Manufacturer;
        CostInCredits = starship.CostInCredits;
        Length = starship.Length;
        Crew = starship.Crew;
        Passengers = starship.Passengers;
        MaxAtmospheringSpeed = starship.MaxAtmospheringSpeed;
        HyperdriveRating = starship.HyperdriveRating;
        MGLT = starship.MGLT;
        CargoCapacity = starship.CargoCapacity;
        Consumables = starship.Consumables;
        Created = starship.Created;
        Edited = starship.Edited;
        Films = starship.Films
            .Select(film => new FilmSummaryDto(film)).ToList();
        Pilots = starship.Pilots
            .Select(pilot => new PersonSummaryDto(pilot)).ToList();
    }

    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Slug { get; } = string.Empty;
    public string Model { get; } = string.Empty;
    public string StarshipClass { get; } = string.Empty;
    public string Manufacturer { get; } = string.Empty;
    public int CostInCredits { get; }
    public double Length { get; }
    public int Crew { get; }
    public int Passengers { get; }
    public int MaxAtmospheringSpeed { get; }
    public string HyperdriveRating { get; } = string.Empty;
    public string MGLT { get; } = string.Empty;
    public int CargoCapacity { get; }
    public string Consumables { get; } = string.Empty;
    public DateTime Created { get; }
    public DateTime Edited { get; }

    public List<FilmSummaryDto> Films { get; } = [];
    public List<PersonSummaryDto> Pilots { get; } = [];
}
