using MayTheFourth.Core;
using MayTheFourth.DataImporter.Services;
using MayTheFourth.DataImporter.Services.Contexts.SharedContext;

namespace MayTheFourth.Api.Extensions
{
    public static class DataImportExtension
    {
        public static void AddDataImport(this WebApplicationBuilder builder)
        {
            var dataImportSettings = builder.Configuration.GetSection("DataImportSettings");
            string implementationType = dataImportSettings["ImportType"]!;

            if (implementationType == "JsonFile")
                builder.Services.AddTransient<IDataImportService, JsonFileImportService>();
            else if (implementationType == "Swapi")
            {
                // Registrar httpClient
                builder.Services.AddHttpClient();
                builder.Services.AddTransient<IDataImportService, SwapiImportService>(); 
            }
        }

    public static async Task ImportDataAsync(this WebApplication app, WebApplicationBuilder builder)
        {
            var dataImportSettings = builder.Configuration.GetSection("DataImportSettings");
            string importType = dataImportSettings["ImportType"]!;
            using var scope = app.Services.CreateScope();

            if (importType == "JsonFile")
            {
                var jsonConfig = dataImportSettings.GetSection("JsonFileImportSettings");
                var resourceFileName = jsonConfig["ResourceFileName"];

                var jsonFileImportService = (JsonFileImportService)scope.ServiceProvider.GetRequiredService<IDataImportService>();
                jsonFileImportService.ResourceFileName = resourceFileName!;
                await jsonFileImportService.ImportDataAsync(new CancellationToken());
            }
            else if (importType == "Swapi")
            {
                var swapiConfig = dataImportSettings.GetSection("SwapiImportSettings");
                var apiUrl = swapiConfig["ApiUrl"];

                var swapiImportService = (SwapiImportService)scope.ServiceProvider.GetRequiredService<IDataImportService>();
                swapiImportService.Url = apiUrl!;
                await swapiImportService.ImportDataAsync(new CancellationToken());
            }
        }
    }
}
