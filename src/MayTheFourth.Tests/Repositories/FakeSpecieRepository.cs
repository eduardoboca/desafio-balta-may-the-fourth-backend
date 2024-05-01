using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;

namespace MayTheFourth.Tests.Repositories;

public class FakeSpecieRepository : ISpeciesRepository
{
    public readonly List<Species> species = new List<Species>()
    {
        new Species(){ Id = new Guid("378e6610-0b13-400c-96c0-b17a3055f14b"), Name = "Human", Slug = "human"},
        new Species(){ Name = "Droid", Slug = "droid"},
        new Species(){ Name = "Wookie", Slug = "wookie"},
        new Species(){ Name = "Rodian", Slug = "rodian"},
        new Species(){ Name = "Hutt", Slug = "hutt"}
    };

    public Task<bool> AnyAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> CountItemsAsync()
    {
        return Task.FromResult(species.Count);
    }

    public async Task<PagedList<Species>> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = species.AsQueryable();
        return await GetPagedAsync(query, pageNumber, pageSize);
    }

    public Task<Species?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(species.FirstOrDefault(x => x.Id == id));
    }

    public async Task<Species?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(slug))
            return null;

        var lowerCaseSlug = slug.ToLowerInvariant();
        return await Task.FromResult(species.FirstOrDefault(x => x.Slug == lowerCaseSlug));
    }

    public Task<Species?> GetByUrlAsync(string url, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(Species species, CancellationToken cancellationToken)
    {
        if (species is null)
            return Task.FromResult(false);

        return Task.FromResult(true);
    }

    public Task UpdateAsync(Species species, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static Task<PagedList<T>> GetPagedAsync<T>(IQueryable<T> source, int pageNumber, int pageSize)
    => Task.FromResult(new PagedList<T>(pageNumber, pageSize, source.Count(), source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()));
}
