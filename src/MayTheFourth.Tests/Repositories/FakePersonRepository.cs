using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;

namespace MayTheFourth.Tests.Repositories
{
    public class FakePersonRepository : IPersonRepository
    {
        public readonly List<Person> people = new List<Person>()
        {
            new Person(){ Id = new Guid("378e6610-0b13-400c-96c0-b17a3055f14b"), Name = "Obi-Wan Kenobi", BirthYear = "57BBY", Slug = "obi-wan-kenobi"},
            new Person(){ Name = "Anakin Skywalker", BirthYear = "41.9BBY", Slug = "anakin-skywalker"},
            new Person(){ Name = "Chewbacca", BirthYear = "200BBY", Slug = "chewbacca"},
            new Person(){ Name = "Han Solo", BirthYear = "29BBY", Slug = "han-solo"},
            new Person(){ Name = "Yoda", BirthYear = "896BBY", Slug = "yoda"},
        };

        public Task<bool> AnyAsync(string name, string birthYear)
        {
            if (string.Equals(name, people[0].Name) && birthYear.Equals(people[0].BirthYear))
                return Task.FromResult(true);

            return Task.FromResult(false);
        }

        public Task<int> CountItemsAsync()
        {
            return Task.FromResult(people.Count);
        }

        public async Task<PagedList<Person>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = people.AsQueryable();
            return await GetPagedAsync(query, pageNumber, pageSize);
        }

        public Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.FromResult(people.FirstOrDefault(x => x.Id == id));
        }

        public async Task<Person?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(slug))
                return null;

            var lowerCaseSlug = slug.ToLowerInvariant();
            return await Task.FromResult(people.FirstOrDefault(x => x.Slug == lowerCaseSlug));
        }

        public Task<Person?> GetByUrlAsync(string url, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Person person, CancellationToken cancellationToken)
        {
            if (person is null)
                return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public Task UpdateAsync(Person person, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        private static Task<PagedList<T>> GetPagedAsync<T>(IQueryable<T> source, int pageNumber, int pageSize)
            => Task.FromResult(new PagedList<T>(pageNumber, pageSize, source.Count(), source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()));

        public Task<bool> AnyAsync()
        {
            throw new NotImplementedException();
        }
    }
}
