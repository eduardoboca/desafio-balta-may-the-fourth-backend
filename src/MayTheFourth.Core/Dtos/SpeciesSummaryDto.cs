using MayTheFourth.Core.Entities;
using System.IO;

namespace MayTheFourth.Core.Dtos;

public class SpeciesSummaryDto
{
    public SpeciesSummaryDto(Species species)
    {
        Id = species.Id;
        Name = species.Name;
        Slug = species.Slug;
        Classification = species.Classification;
        Designation = species.Designation;
        Language = species.Language;
    }

    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Slug { get; } = string.Empty;
    public string Classification { get; } = string.Empty;
    public string Designation { get; } = string.Empty;
    public string Language { get; } = string.Empty;
}
