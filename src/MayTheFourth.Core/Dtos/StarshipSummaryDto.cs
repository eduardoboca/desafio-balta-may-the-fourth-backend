using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Dtos;

public class StarshipSummaryDto
{
    public StarshipSummaryDto(Starship starship)
    {
        Id = starship.Id;
        Name = starship.Name;
        Slug = starship.Slug;
        Model = starship.Model;
        Manufacturer = starship.Manufacturer;
    }

    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Slug { get; } = string.Empty;
    public string Model { get; } = string.Empty;
    public string Manufacturer { get; } = string.Empty;
}
