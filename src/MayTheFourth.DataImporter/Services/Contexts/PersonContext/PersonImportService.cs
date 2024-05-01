using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.DataImporter.DTOs;
using MayTheFourth.DataImporter.Services.Contexts.SharedContext;
using System.Text.Json;

namespace MayTheFourth.DataImporter.Services.Contexts.PersonContext
{
    public class PersonImportService : IImportService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPlanetRepository _planetRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IStarshipRepository _starshipRepository;
        private readonly IVehicleRepository _vehicleRepository;

        private List<PersonDTO> _people = new();
        public PersonImportService(IPersonRepository personRepository, 
                                    IPlanetRepository planetRepository, 
                                    IFilmRepository filmRepository,
                                    ISpeciesRepository speciesRepository,
                                    IStarshipRepository starshipRepository,
                                    IVehicleRepository vehicleRepository)
        {
            _personRepository = personRepository;
            _planetRepository = planetRepository;
            _filmRepository = filmRepository;
            _speciesRepository = speciesRepository;
            _starshipRepository = starshipRepository;
            _vehicleRepository = vehicleRepository;
        }

        public void Add(PersonDTO person) => _people.Add(person);

        public void LoadList(string jsonList)
            => _people = JsonSerializer.Deserialize<List<PersonDTO>>(jsonList)!;

        public async Task ImportAsync(CancellationToken cancellationToken)
        {
            // Cadastrar 
            foreach (var personDTO in _people)
            {
                // Encontrar homeworld (planet) pela url
                var planet = await _planetRepository.GetByUrlAsync(personDTO.Homeworld, cancellationToken);
                var person = personDTO.ToPerson();
                person.HomeworldId = planet!.Id;
                await _personRepository.SaveAsync(person, cancellationToken);
            }
        }

        public async Task<bool> IsEmpty() => !await _personRepository.AnyAsync();

        public async Task UpdateRelationsAsync(CancellationToken cancellationToken)
        {
            foreach (var personDTO in _people)
            {
                var person = await _personRepository.GetByUrlAsync(personDTO.Url, cancellationToken);
                foreach (var url in personDTO.Films)
                {
                    var film = await _filmRepository.GetByUrlAsync(url, cancellationToken);
                    person!.Films.Add(film!);
                }

                foreach (var url in personDTO.Species)
                {
                    var specie = await _speciesRepository.GetByUrlAsync(url, cancellationToken);
                    person!.Species.Add(specie!);
                }

                foreach (var url in personDTO.Starships!)
                {
                    var starship = await _starshipRepository.GetByUrlAsync(url, cancellationToken);
                    person!.Starships.Add(starship!);
                }

                foreach (var url in personDTO.Vehicles!)
                {
                    var vehicle = await _vehicleRepository.GetByUrlAsync(url, cancellationToken);
                    person!.Vehicles.Add(vehicle!);
                }

                await _personRepository.UpdateAsync(person, cancellationToken);
            }

        }
    }
}
