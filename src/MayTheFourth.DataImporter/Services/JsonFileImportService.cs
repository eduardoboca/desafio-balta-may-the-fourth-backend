using MayTheFourth.DataImporter.Services.Contexts.FilmContext;
using MayTheFourth.DataImporter.Services.Contexts.PersonContext;
using MayTheFourth.DataImporter.Services.Contexts.PlanetContext;
using MayTheFourth.DataImporter.Services.Contexts.SpeciesContext;
using MayTheFourth.DataImporter.Services.Contexts.StarshipContext;
using MayTheFourth.DataImporter.Services.Contexts.VehicleContext;
using System.Reflection;
using System.Text.Json;

namespace MayTheFourth.DataImporter.Services
{
    public class JsonFileImportService : IDataImportService
    {
        private readonly PlanetImportService _planetService;
        private readonly PersonImportService _personService;
        private readonly SpeciesImportService _speciesService;
        private readonly FilmImportService _filmService;
        private readonly StarshipImportService _starshipService;
        private readonly VehicleImportService _vehicleService;

        public string ResourceFileName { get; set; } = string.Empty;

        public JsonFileImportService(
                                     FilmImportService filmService,
                                     PlanetImportService planetService, 
                                     PersonImportService personService,
                                     SpeciesImportService speciesService,
                                     StarshipImportService starshipService,
                                     VehicleImportService vehicleService)
        {
            _planetService = planetService;
            _personService = personService;
            _speciesService = speciesService;
            _filmService = filmService;
            _vehicleService = vehicleService;
            _starshipService = starshipService;
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

            try
            {
                #region LoadLists
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(ResourceFileName)!)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string jsonString = reader.ReadToEnd();
                        using (JsonDocument doc = JsonDocument.Parse(jsonString))
                        {
                            JsonElement root = doc.RootElement;
                            foreach (JsonElement element in root.EnumerateArray())
                            {
                                var endpoint = element.GetProperty("Endpoint").GetString();
                                JsonElement data = element.GetProperty("Data");
                                JsonElement results = data.GetProperty("results");
                                string jsonList = results.GetRawText();
                                switch (endpoint)
                                {
                                    case "people":
                                        _personService.LoadList(jsonList);
                                        break;

                                    case "planets":
                                        _planetService.LoadList(jsonList);
                                        break;

                                    case "species":
                                        _speciesService.LoadList(jsonList);
                                        break;

                                    case "films":
                                        _filmService.LoadList(jsonList);
                                        break;

                                    case "starships":
                                        _starshipService.LoadList(jsonList);
                                        break;

                                    case "vehicles":
                                        _vehicleService.LoadList(jsonList);
                                        break;
                                }
                            }
                        }
                    }

                }
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
