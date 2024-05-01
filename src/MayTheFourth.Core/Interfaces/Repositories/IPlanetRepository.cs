using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Interfaces.Repositories;

public interface IPlanetRepository
{
    Task<bool> AnyAsync();
    Task<bool> AnyAsync(string name, string gravity);
    Task<int> CountTotalItemsAsync();
    Task<PagedList<Planet>?> GetAllAsync(int pageNumber, int pageSize);
    Task<Planet?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Planet?> GetBySlugAsync(string slug, CancellationToken cancellationToken);
    Task<Planet?> GetByUrlAsync(string url, CancellationToken cancellationToken);
    Task SaveAsync(Planet planet, CancellationToken cancellationToken);
    Task UpdateAsync(Planet planet, CancellationToken cancellationToken);
}
