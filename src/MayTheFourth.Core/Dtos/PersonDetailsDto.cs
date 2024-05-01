using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Dtos;

public class PersonDetailsDto
{
    public PersonDetailsDto(Person person)
    {
        Id = person.Id;
        Name = person.Name;
        Slug = person.Slug;
        BirthYear = person.BirthYear;
        EyeColor = person.EyeColor;
        Gender = person.Gender;
        HairColor = person.HairColor;
        Height = person.Height;
        Mass = person.Mass;
        SkinColor = person.SkinColor;
        Created = person.Created;
        Edited = person.Edited;
        HomeworldId = person.HomeworldId;
        Films = person.Films
            .Select(film => new FilmSummaryDto(film)).ToList();
        Species = person.Species
            .Select(species => new SpeciesSummaryDto(species)).ToList();
        Starships = person.Starships
            .Select(starship => new StarshipSummaryDto(starship)).ToList();
        Vehicles = person.Vehicles
            .Select(vehicle => new VehicleSummaryDto(vehicle)).ToList();
    }

    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Slug { get; } = string.Empty;
    public string BirthYear { get; } = string.Empty;
    public string EyeColor { get; } = string.Empty;
    public string Gender { get; } = string.Empty;
    public string HairColor { get; } = string.Empty;
    public int Height { get; }
    public string Mass { get; } = string.Empty;
    public string SkinColor { get; } = string.Empty;
    public DateTime Created { get; }
    public DateTime Edited { get; }

    public Planet? Homeworld { get; }
    public Guid HomeworldId { get; }

    public List<FilmSummaryDto> Films { get; } = [];
    public List<SpeciesSummaryDto> Species { get; } = [];
    public List<StarshipSummaryDto> Starships { get; } = [];
    public List<VehicleSummaryDto> Vehicles { get; } = [];
}
