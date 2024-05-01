using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Dtos;

public class PersonSummaryDto
{
    public PersonSummaryDto(Person person)
    {
        Id = person.Id;
        Name = person.Name;
        Slug = person.Slug;
        BirthYear = person.BirthYear;
        Gender = person.Gender;
        HomeworldId = person.HomeworldId;
    }

    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Slug { get; } = string.Empty;
    public string BirthYear { get; } = string.Empty;
    public string Gender { get; } = string.Empty;
    public Guid HomeworldId { get; }
}
