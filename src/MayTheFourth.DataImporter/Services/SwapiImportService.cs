using MayTheFourth.DataImporter.DTOs;
using MayTheFourth.DataImporter.Services.Contexts.FilmContext;
using MayTheFourth.DataImporter.Services.Contexts.PersonContext;
using MayTheFourth.DataImporter.Services.Contexts.PlanetContext;
using MayTheFourth.DataImporter.Services.Contexts.SpeciesContext;
using MayTheFourth.DataImporter.Services.Contexts.StarshipContext;
using MayTheFourth.DataImporter.Services.Contexts.VehicleContext;
using MayTheFourth.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MayTheFourth.DataImporter.Services
{
    public class SwapiImportService : IDataImportService
    {
        private readonly PlanetImportService _planetService;
        private readonly PersonImportService _personService;
        private readonly SpeciesImportService _speciesService;
        private readonly FilmImportService _filmService;
        private readonly StarshipImportService _starshipService;
        private readonly VehicleImportService _vehicleService;

        private readonly HttpClient _httpClient;
        public string Url { get; set; } = string.Empty;

        public SwapiImportService(FilmImportService filmService,
                                     PlanetImportService planetService,
                                     PersonImportService personService,
                                     SpeciesImportService speciesService,
                                     StarshipImportService starshipService,
                                     VehicleImportService vehicleService,
                                     HttpClient httpClient)
        {
            _planetService = planetService;
            _personService = personService;
            _speciesService = speciesService;
            _filmService = filmService;
            _vehicleService = vehicleService;
            _starshipService = starshipService;
            _httpClient = httpClient;
        }

        private async Task<bool> IsDatabaseEmpty()
        {
            return await _filmService.IsEmpty()
                & await _personService.IsEmpty()
                & await _planetService.IsEmpty()
                & await _speciesService.IsEmpty()
                & await _starshipService.IsEmpty()
                & await _vehicleService.IsEmpty();

        }
        public async Task ImportDataAsync(CancellationToken cancellationToken)
        {
            if (!await IsDatabaseEmpty()) return;

            _httpClient.BaseAddress = new Uri(Url);

            #region LoadLists

            #region people
            var nextPage = "api/people?page=1";

            while (nextPage != null) 
            {
                var swapiDTO = await _httpClient.GetFromJsonAsync<SwapiDTO>(nextPage);
                nextPage = swapiDTO!.Next;
                foreach (var element in swapiDTO!.Results!)
                {
                    PersonDTO personDTO = JsonSerializer.Deserialize<PersonDTO>(element)!;
                    _personService.Add(personDTO);
                }
            }
            #endregion

            #region films
            nextPage = "api/films?page=1";

            while (nextPage != null)
            {
                var swapiDTO = await _httpClient.GetFromJsonAsync<SwapiDTO>(nextPage);
                nextPage = swapiDTO!.Next;
                foreach (var element in swapiDTO!.Results!)
                {
                    FilmDTO filmDTO = JsonSerializer.Deserialize<FilmDTO>(element)!;
                    _filmService.Add(filmDTO);
                }
            }
            #endregion

            #region planets
            nextPage = "api/planets?page=1";

            while (nextPage != null)
            {
                var swapiDTO = await _httpClient.GetFromJsonAsync<SwapiDTO>(nextPage);
                nextPage = swapiDTO!.Next;
                foreach (var element in swapiDTO!.Results!)
                {
                    PlanetDTO planetDTO = JsonSerializer.Deserialize<PlanetDTO>(element)!;
                _planetService.Add(planetDTO);
                }
            }
            #endregion

            #region species
            nextPage = "api/species?page=1";

            while (nextPage != null)
            {
                var swapiDTO = await _httpClient.GetFromJsonAsync<SwapiDTO>(nextPage);
                nextPage = swapiDTO!.Next;
                foreach (var element in swapiDTO!.Results!)
                {
                    SpeciesDTO speciesDTO = JsonSerializer.Deserialize<SpeciesDTO>(element)!;
                    _speciesService.Add(speciesDTO);
                }
            }
            #endregion

            #region starships
            nextPage = "api/starships?page=1";

            while (nextPage != null)
            {
                var swapiDTO = await _httpClient.GetFromJsonAsync<SwapiDTO>(nextPage);
                nextPage = swapiDTO!.Next;
                foreach (var element in swapiDTO!.Results!)
                {
                    StarshipDTO starshipDTO = JsonSerializer.Deserialize<StarshipDTO>(element)!;
                    _starshipService.Add(starshipDTO);
                }
            }
            #endregion

            #region vehicles
            nextPage = "api/vehicles?page=1";

            while (nextPage != null)
            {
                var swapiDTO = await _httpClient.GetFromJsonAsync<SwapiDTO>(nextPage);
                nextPage = swapiDTO!.Next;
                foreach (var element in swapiDTO!.Results!)
                {
                    VehicleDTO vehicleDTO = JsonSerializer.Deserialize<VehicleDTO>(element)!;
                    _vehicleService.Add(vehicleDTO);
                }
            }
            #endregion

            #endregion

            #region Create Records
            // Planet tem que ser o primeiro, porque Person e Species têm referência pra ele (Homeworld)
            await _planetService.ImportAsync(cancellationToken);
            await _personService.ImportAsync(cancellationToken);
            await _speciesService.ImportAsync(cancellationToken);
            await _filmService.ImportAsync(cancellationToken);
            await _starshipService.ImportAsync(cancellationToken);
            await _vehicleService.ImportAsync(cancellationToken);
            #endregion

            #region Create Relationships
            await _planetService.UpdateRelationsAsync(cancellationToken);
            await _personService.UpdateRelationsAsync(cancellationToken);
            await _filmService.UpdateRelationsAsync(cancellationToken);
            #endregion
        }
    }
}
