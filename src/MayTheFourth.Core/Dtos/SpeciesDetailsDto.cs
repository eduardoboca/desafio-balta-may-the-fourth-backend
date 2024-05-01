using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Dtos;

public class SpeciesDetailsDto
{
    public SpeciesDetailsDto(Species species)
    {
        Id = species.Id;
        Name = species.Name;
        Slug = species.Slug;
        Classification = species.Classification;
        Designation = species.Designation;
        AverageHeight = species.AverageHeight;
        AverageLifespan = species.AverageLifespan;
        EyeColors = species.EyeColors;
        HairColors = species.HairColors;
        SkinColors = species.SkinColors;
        Language = species.Language;
        Created = species.Created;
        Edited = species.Edited;
        Homeworld = species.Homeworld;
        HomeworldId = species.HomeworldId;
        People = species.People
            .Select(person => new PersonSummaryDto(person)).ToList();
        Films = species.Films
            .Select(film => new FilmSummaryDto(film)).ToList();
    }

    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Slug { get; } = string.Empty;
    public string Classification { get; } = string.Empty;
    public string Designation { get; } = string.Empty;
    public int AverageHeight { get; }
    public int AverageLifespan { get; }
    public string EyeColors { get; } = string.Empty;
    public string HairColors { get; } = string.Empty;
    public string SkinColors { get; } = string.Empty;
    public string Language { get; } = string.Empty;
    public DateTime Created { get; }
    public DateTime Edited { get; }

    public Planet? Homeworld { get; }
    public Guid? HomeworldId { get; }


    public List<PersonSummaryDto> People { get; } = [];
    public List<FilmSummaryDto> Films { get; } = [];
}
