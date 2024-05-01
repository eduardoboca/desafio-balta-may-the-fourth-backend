using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;

namespace MayTheFourth.Core.Interfaces.Repositories;

public interface IPersonRepository
{
    Task<int> CountItemsAsync();
    Task<bool> AnyAsync();
    Task<bool> AnyAsync(string name, string birthYear);
    Task<PagedList<Person>> GetAllAsync(int pageNumber, int pageSize);
    Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Person?> GetBySlugAsync(string slug, CancellationToken cancellationToken);
    Task<Person?> GetByUrlAsync(string url, CancellationToken cancellationToken);
    Task SaveAsync(Person person, CancellationToken cancellationToken);
    Task UpdateAsync(Person person, CancellationToken cancellationToken);

}
