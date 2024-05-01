using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.DataImporter.DTOs;
using MayTheFourth.DataImporter.Services.Contexts.SharedContext;
using System.Text.Json;

namespace MayTheFourth.DataImporter.Services.Contexts.StarshipContext
{
    public class StarshipImportService : IImportService
    {
        private readonly IStarshipRepository _starshipRepository;
        private List<StarshipDTO> _starships = new();
        public StarshipImportService(IStarshipRepository starshipRepository)
        {
            _starshipRepository = starshipRepository;
        }

        public void Add(StarshipDTO starship) => _starships.Add(starship);

        public void LoadList(string jsonList)
            => _starships = JsonSerializer.Deserialize<List<StarshipDTO>>(jsonList)!;

        public async Task ImportAsync(CancellationToken cancellationToken)
        {
            foreach (var starshipDTO in _starships)
            {
                await _starshipRepository.SaveAsync(starshipDTO.ToStarship(), cancellationToken);
            }
        }

        public async Task<bool> IsEmpty() => !await _starshipRepository.AnyAsync();
    }
}
