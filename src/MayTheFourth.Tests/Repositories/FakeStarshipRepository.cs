﻿using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;

namespace MayTheFourth.Tests.Repositories;

public class FakeStarshipRepository : IStarshipRepository
{
    public readonly List<Starship> _starships;

    public FakeStarshipRepository()
    {
        _starships = new List<Starship>()
        {
            new Starship()
            {
                Id = Guid.NewGuid(),
                Name = "Millennium Falcon",
                Slug = "millennium-falcon",
                Manufacturer = "Corellian Engineering Corporation"
            },
            new Starship()
            {
                Id = Guid.NewGuid(),
                Name = "X-wing",
                Slug = "x-wing",
                Manufacturer = "Incom Corporation"
            },
            new Starship()
            {
                Id = Guid.NewGuid(),
                Name = "TIE Fighter",
                Slug = "tie-fighter",
                Manufacturer = "Sienar Fleet Systems"
            },
        };
    }

    public Task<bool> AnyAsync(string name, CancellationToken cancellationToken)
    {
        if (string.Equals(name, _starships[0].Name))
            return Task.FromResult(true);

        return Task.FromResult(false);
    }

    public Task<Starship?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Starship?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(Starship starship, CancellationToken cancellationToken)
    {
        if (starship is null)
            return Task.FromResult(false);

        return Task.FromResult(true);
    }

    public Task<int> CountTotalItemsAsync()
        => Task.FromResult(_starships.Count);

    public async Task<PagedList<Starship>?> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = _starships.AsQueryable();
        return await GetPagedAsync(query, pageNumber, pageSize);
    }

    private static Task<PagedList<T>> GetPagedAsync<T>(IQueryable<T> source, int pageNumber, int pageSize)
        => Task.FromResult(new PagedList<T>(pageNumber, pageSize, source.Count(), source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()));
}
