using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Dtos;

public class FilmSummaryDto
{
    public FilmSummaryDto(Film film)
    {
        Id = film.Id;
        Title = film.Title;
        Slug = film.Slug;
        Director = film.Director;
        ReleaseDate = film.ReleaseDate;
    }

    public Guid Id { get; }
    public string Title { get; } = string.Empty;
    public string Slug { get; } = string.Empty;
    public string Director { get; } = string.Empty;
    public DateTime ReleaseDate { get; }
}
