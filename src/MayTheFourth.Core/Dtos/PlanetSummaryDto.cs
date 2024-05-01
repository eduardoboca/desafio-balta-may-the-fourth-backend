using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Dtos;

public class PlanetSummaryDto
{
    public PlanetSummaryDto(Planet planet)
    {
        Id = planet.Id;
        Name = planet.Name;
        Slug = planet.Slug;
        Gravity = planet.Gravity;
        Population = planet.Population;
        Climate = planet.Climate;
    }

    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Slug { get; } = string.Empty;
    public string Gravity { get; } = string.Empty;
    public int Population { get; }
    public string Climate { get; } = string.Empty;
}
