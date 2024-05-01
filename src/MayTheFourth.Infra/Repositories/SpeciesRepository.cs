using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace MayTheFourth.Infra.Repositories
{
    public class SpeciesRepository : BaseRepository<Species>, ISpeciesRepository
    {
        public SpeciesRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<bool> AnyAsync()
            => await _appDbContext.Planets.AnyAsync();

        public async Task SaveAsync(Species species, CancellationToken cancellationToken)
        {
            await _appDbContext.Species.AddAsync(species);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<PagedList<Species>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _appDbContext.Species.AsQueryable();
            return await GetPagedAsync(query, pageNumber, pageSize);
        }

        public async Task<int> CountItemsAsync()
        => await _appDbContext.Species.CountAsync();


        public async Task<Species?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => await _appDbContext.Species
            .Include(x => x.Films)
            .Include(x => x.People)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Species?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
            => await _appDbContext.Species
                .Include(x => x.Films)
                .Include(x => x.People)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);

        public async Task<Species?> GetByUrlAsync(string url, CancellationToken cancellationToken)
        => await _appDbContext.Species
            .FirstOrDefaultAsync(x => x.Url == url);

        public async Task UpdateAsync(Species species, CancellationToken cancellationToken)
        {
            _appDbContext.Update(species);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
