using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayTheFourth.Core.Interfaces.Repositories
{
    public interface IFilmRepository
    {
        Task<bool> AnyAsync();
        Task<int> CountItemsAsync();
        Task<PagedList<Film>> GetAllAsync(int pageNumber, int pageSize);
        Task<Film?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Film?> GetBySlugAsync(string slug, CancellationToken cancellationToken);
        Task<Film?> GetByUrlAsync(string url, CancellationToken cancellationToken);
        Task SaveAsync(Film film, CancellationToken cancellationToken);
        Task UpdateAsync(Film film, CancellationToken cancellationToken);


    }
}
