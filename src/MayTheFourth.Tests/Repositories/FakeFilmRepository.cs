using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;

namespace MayTheFourth.Tests.Repositories;

public class FakeFilmRepository : IFilmRepository
{
    public readonly List<Film> films = new List<Film>
    {
         new Film { Id = new Guid("bb20333b-182c-4d5f-9a53-b7bd9c149639"), Created = DateTime.Now, Director = "Igor", Edited = DateTime.Now, Title = "Filme 01", Slug = "filme-01" },
         new Film { Created = DateTime.Now, Director = "Eduardo", Edited = DateTime.Now, Title = "Filme 02", Slug = "filme-02" },
         new Film { Created = DateTime.Now, Director = "Erik", Edited = DateTime.Now, Title = "Filme 03", Slug = "filme-03" },
         new Film { Created = DateTime.Now, Director = "Everson", Edited = DateTime.Now, Title = "Filme 04", Slug = "filme-04" },
         new Film { Created = DateTime.Now, Director = "Lucas", Edited = DateTime.Now, Title = "Filme 05", Slug = "filme-05" }
    };

    public Task<bool> AnyAsync()
    {
        if (!string.IsNullOrEmpty(films.Select(x => x.Title).FirstOrDefault()))
            return Task.FromResult(true);
       
        return Task.FromResult(false);
    }

    public Task<int> CountItemsAsync()
    {
        return Task.FromResult(films.Count);
    }

    public async Task<List<Film>> GetAllAsync()
    {
        await Task.Delay(10);
        return films;
    }

    public async Task<PagedList<Film>> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = films.AsQueryable();
        return await GetPagedAsync(query, pageNumber, pageSize);
    }

    public Task<Film?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(films.FirstOrDefault(x => x.Id == id));
    }

    public async Task<Film?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(slug))
            return null;

        var lowerCaseSlug = slug.ToLowerInvariant();
        return await Task.FromResult(films.FirstOrDefault(x => x.Slug == lowerCaseSlug));
    }

    public Task<Film?> GetByUrlAsync(string url, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(Film film, CancellationToken cancellationToken)
    {
        if (films is null)
            return Task.FromResult(false);

        return Task.FromResult(true);
    }

    public Task UpdateAsync(Film film, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static Task<PagedList<T>> GetPagedAsync<T>(IQueryable<T> source, int pageNumber, int pageSize)
        => Task.FromResult(new PagedList<T>(pageNumber, pageSize, source.Count(), source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()));
}
