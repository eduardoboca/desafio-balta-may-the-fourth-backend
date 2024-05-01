using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Interfaces.Repositories;

public interface ISpeciesRepository
{
    Task<bool> AnyAsync();
    Task<int> CountItemsAsync();
    Task<PagedList<Species>> GetAllAsync(int pageNumber, int pageSize);
    Task<Species?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task SaveAsync(Species species, CancellationToken cancellationToken);
    Task<Species?> GetBySlugAsync(string slug, CancellationToken cancellationToken);
    Task<Species?> GetByUrlAsync(string url, CancellationToken cancellationToken);
    Task UpdateAsync(Species species, CancellationToken cancellationToken);


}
