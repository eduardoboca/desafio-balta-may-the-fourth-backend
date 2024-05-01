using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using System;

namespace MayTheFourth.Tests.Repositories;

public class FakeVehicleRepository : IVehicleRepository
{
    public readonly List<Vehicle> vehicles = new List<Vehicle>()
    {
        new Vehicle(){ Id = new Guid("378e6610-0b13-400c-96c0-b17a3055f14b"), Name = "Sand Crawler", Model = "Digger Crawler", Slug = "sand-crawler"},
        new Vehicle(){ Name = "T-16 skyhopper", Model = "T-16 skyhopper", Slug = "t-16-skyhopper"},
        new Vehicle(){ Name = "X-34 landspeeder", Model = "X-34 landspeeder", Slug = "x-34-landspeeder"},
        new Vehicle(){ Name = "TIE/LN starfighter", Model = "Twin Ion Engine/Ln Starfighter", Slug = "tie/ln-starfighter"},
        new Vehicle(){ Name = "Snowspeeder", Model = "t-47 airspeeder", Slug = "snowspeeder"},
    };

    public Task<bool> AnyAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(string name, string model)
    {
        if (string.Equals(name, vehicles[0].Name) && model.Equals(vehicles[0].Model))
            return Task.FromResult(true);

        return Task.FromResult(false);
    }

    public Task<int> CountTotalItemsAsync()
    {
        return Task.FromResult(vehicles.Count);
    }

    public Task<bool> DeleteVehicleByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedList<Vehicle>?> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = vehicles.AsQueryable();
        return await GetPagedAsync(query, pageNumber, pageSize);
    }

    public Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(vehicles.FirstOrDefault(x => x.Id == id));
    }

    public async Task<Vehicle?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(slug))
            return null;

        var lowerCaseSlug = slug.ToLowerInvariant();
        return await Task.FromResult(vehicles.FirstOrDefault(x => x.Slug == lowerCaseSlug));
    }

    public Task<Vehicle?> GetByUrlAsync(string url, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(Vehicle vehicle, CancellationToken cancellationToken)
    {
        if (vehicle is null)
            return Task.FromResult(false);

        return Task.FromResult(true);
    }

    public Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static Task<PagedList<T>> GetPagedAsync<T>(IQueryable<T> source, int pageNumber, int pageSize)
        => Task.FromResult(new PagedList<T>(pageNumber, pageSize, source.Count(), source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()));
}
