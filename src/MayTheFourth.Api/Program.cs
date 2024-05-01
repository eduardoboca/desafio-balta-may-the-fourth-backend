using MayTheFourth.Api.Extensions;
using MayTheFourth.Api.Extensions.Contexts.FilmContext;
using MayTheFourth.Api.Extensions.Contexts.PersonContext;
using MayTheFourth.Api.Extensions.Contexts.PlanetContext;
using MayTheFourth.Api.Extensions.Contexts.SpeciesContext;
using MayTheFourth.Api.Extensions.Contexts.StartshipContext;
using MayTheFourth.Api.Extensions.Contexts.VehicleContext;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;


#region builder
var builder = WebApplication.CreateBuilder(args);

builder.AddCorsConfiguration();
builder.AddConfiguration();
builder.AddPlanetContext();
builder.AddStarshipContext();
builder.AddFilmContext();
builder.AddPersonContext();
builder.AddSpeciesContext();
builder.AddVehicleContext();
builder.AddDbContext();
builder.AddDataImport();
builder.AddMediatR();

builder.Services.Configure<JsonOptions>(opt => 
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.AddSwaggerConfigurations();
#endregion

#region app
var app = builder.Build();

app.UseSwagger();
app.MapSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MayTheFourth API V1");
});
app.UseHttpsRedirection();

await app.ImportDataAsync(builder);

app.UseCors("RebelRenegades");
app.MapPlanetEndpoints();
app.MapStarshipEndpoints();
app.MapFilmEndpoints();
app.MapPersonEndpoints();
app.MapSpeciesEndpoints();
app.MapVehicleEndpoints();

app.Run();
#endregion
