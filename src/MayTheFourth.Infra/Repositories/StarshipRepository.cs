using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace MayTheFourth.Infra.Repositories;

public class StarshipRepository : BaseRepository<Starship>, IStarshipRepository
{
    public StarshipRepository(AppDbContext appDbContext) : base(appDbContext) { }

    public async Task<bool> AnyAsync()
        => await _appDbContext.Planets.AnyAsync();

    public async Task<bool> AnyAsync(string name, CancellationToken cancellationToken)
        => await _appDbContext.Starships.AnyAsync(x => x.Name == name, cancellationToken);

    public async Task SaveAsync(Starship starship, CancellationToken cancellationToken)
    {
        await _appDbContext.Starships.AddAsync(starship);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Starship?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _appDbContext.Starships
            .Include(x => x.Films)
            .Include(x => x.Pilots)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<Starship?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
        => await _appDbContext.Starships
            .Include(x => x.Films)
            .Include(x => x.Pilots)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);

    public async Task<int> CountTotalItemsAsync()
        => await _appDbContext.Starships.CountAsync();

    public async Task<PagedList<Starship>?> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = _appDbContext.Starships.AsQueryable();
        return await GetPagedAsync(query, pageNumber, pageSize);
    }

    public async Task<Starship?> GetByUrlAsync(string url, CancellationToken cancellationToken)
    => await _appDbContext.Starships
        .FirstOrDefaultAsync(x => x.Url == url);

    public async Task UpdateAsync(Starship starship, CancellationToken cancellationToken)
    {
        _appDbContext.Update(starship);
        await _appDbContext.SaveChangesAsync();
    }
}
