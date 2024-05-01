using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.DataImporter.DTOs;
using MayTheFourth.DataImporter.Services.Contexts.SharedContext;
using System.Text.Json;

namespace MayTheFourth.DataImporter.Services.Contexts.FilmContext
{
    public class FilmImportService : IImportService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IStarshipRepository _starshipRepository;
        private readonly IVehicleRepository _vehicleRepository;

        private List<FilmDTO> _films = new();
        public FilmImportService(IFilmRepository filmRepository,
                                    ISpeciesRepository speciesRepository,
                                    IStarshipRepository starshipRepository,
                                    IVehicleRepository vehicleRepository)
        {
            _filmRepository = filmRepository;
            _speciesRepository = speciesRepository;
            _starshipRepository = starshipRepository;
            _vehicleRepository = vehicleRepository;
        }

        public void Add(FilmDTO film) => _films.Add(film);

        public void LoadList(string jsonList)
            => _films = JsonSerializer.Deserialize<List<FilmDTO>>(jsonList)!;

        public async Task ImportAsync(CancellationToken cancellationToken)
        {
            foreach (var filmDTO in _films)
            {
                await _filmRepository.SaveAsync(filmDTO.ToFilm(), cancellationToken);
            }
        }

        public async Task<bool> IsEmpty() => !await _filmRepository.AnyAsync();

        public async Task UpdateRelationsAsync(CancellationToken cancellationToken)
        {
            foreach (var filmDTO in _films)
            {
                var film = await _filmRepository.GetByUrlAsync(filmDTO.Url, cancellationToken);
                foreach (var url in filmDTO.SpeciesList)
                {
                    var species = await _speciesRepository.GetByUrlAsync(url, cancellationToken);
                    film!.SpeciesList.Add(species!);
                }

                foreach (var url in filmDTO.Starships)
                {
                    var starship = await _starshipRepository.GetByUrlAsync(url, cancellationToken);
                    film!.Starships.Add(starship!);
                }

                foreach (var url in filmDTO.Vehicles)
                {
                    var vehicle = await _vehicleRepository.GetByUrlAsync(url, cancellationToken);
                    film!.Vehicles.Add(vehicle!);
                }
                await _filmRepository.UpdateAsync (film!, cancellationToken);
            }
        }
    }
}
