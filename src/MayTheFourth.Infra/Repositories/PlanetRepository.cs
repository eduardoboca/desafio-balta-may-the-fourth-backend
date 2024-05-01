using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace MayTheFourth.Infra.Repositories;

public class PlanetRepository : BaseRepository<Planet>, IPlanetRepository
{
    public PlanetRepository(AppDbContext appDbContext) : base(appDbContext) { }

    public async Task<bool> AnyAsync()
        => await _appDbContext.Planets.AnyAsync();

    public async Task<bool> AnyAsync(string name, string gravity)
        => await _appDbContext.Planets.AnyAsync(x => x.Name == name && x.Gravity.Equals(gravity));

    public async Task SaveAsync(Planet planet, CancellationToken cancellationToken)
    {
        await _appDbContext.Planets.AddAsync(planet, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Planet?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _appDbContext.Planets
            .Include(x => x.Residents)
            .Include(x => x.Films)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);

    public async Task<Planet?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
        => await _appDbContext.Planets
            .Include(x => x.Residents)
            .Include(x => x.Films)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken: cancellationToken);

    public async Task<Planet?> GetByUrlAsync(string url, CancellationToken cancellationToken)
        => await _appDbContext.Planets
            .FirstOrDefaultAsync(x => x.Url == url, cancellationToken: cancellationToken);

    public async Task<int> CountTotalItemsAsync()
        => await _appDbContext.Planets.CountAsync();

    public async Task<PagedList<Planet>?> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = _appDbContext.Planets.AsQueryable();
        return await GetPagedAsync(query, pageNumber, pageSize);
    }

    public async Task UpdateAsync(Planet planet, CancellationToken cancellationToken)
    {
        _appDbContext.Update(planet);
        await _appDbContext.SaveChangesAsync();
    }
}
