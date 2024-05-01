using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.DataImporter.DTOs;
using MayTheFourth.DataImporter.Services.Contexts.SharedContext;
using System.Text.Json;

namespace MayTheFourth.DataImporter.Services.Contexts.PlanetContext
{
    public class PlanetImportService : IImportService
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly IFilmRepository _filmRepository;

        private List<PlanetDTO> _planets = new();
        public PlanetImportService(IPlanetRepository planetRepository,
                                    IFilmRepository filmRepository)
        {
            _planetRepository = planetRepository;
            _filmRepository = filmRepository;
        }

        public void Add(PlanetDTO planet) => _planets.Add(planet);

        public void LoadList(string jsonList)
            => _planets = JsonSerializer.Deserialize<List<PlanetDTO>>(jsonList)!;

        public async Task ImportAsync(CancellationToken cancellationToken)
        {
            foreach (var planetDTO in _planets)
            {
                await _planetRepository.SaveAsync(planetDTO.ToPlanet(), cancellationToken);
            }
        }

        public async Task<bool> IsEmpty() => !await _planetRepository.AnyAsync();

        public async Task UpdateRelationsAsync(CancellationToken cancellationToken)
        {
            foreach (var planetDTO in _planets)
            {
                var planet = await _planetRepository.GetByUrlAsync(planetDTO.Url!, cancellationToken);

                foreach (var url in planetDTO.Films!)
                {
                    var film = await _filmRepository.GetByUrlAsync(url, cancellationToken);
                    planet!.Films.Add(film!);
                }
                await _planetRepository.UpdateAsync(planet!, cancellationToken);
            }
        }
    }
}
