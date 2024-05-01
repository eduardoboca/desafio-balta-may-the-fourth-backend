using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Dtos;

public class PlanetDetailsDto
{
    public PlanetDetailsDto(Planet planet)
    {
        Id = planet.Id;
        Name = planet.Name;
        Slug = planet.Slug;
        Diameter = planet.Diameter;
        RotationPeriod = planet.RotationPeriod;
        OrbitalPeriod = planet.OrbitalPeriod;
        Gravity = planet.Gravity;
        Population = planet.Population;
        Climate = planet.Climate;
        Terrain = planet.Terrain;
        SurfaceWater = planet.SurfaceWater;
        Created = planet.Created;
        Edited = planet.Edited;
        Residents = planet.Residents
            .Select(resident =>  new PersonSummaryDto(resident)).ToList();
        Films = planet.Films
            .Select(film => new FilmSummaryDto(film)).ToList();
    }

    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Slug { get; } = string.Empty;
    public int Diameter { get; }
    public int RotationPeriod { get; }
    public int OrbitalPeriod { get; }
    public string Gravity { get; } = string.Empty;
    public int Population { get; }
    public string Climate { get; } = string.Empty;
    public string Terrain { get; } = string.Empty;
    public string SurfaceWater { get; } = string.Empty;
    public DateTime Created { get; }
    public DateTime Edited { get; }

    public List<PersonSummaryDto> Residents { get; } = [];
    public List<FilmSummaryDto> Films { get; } = [];
}
