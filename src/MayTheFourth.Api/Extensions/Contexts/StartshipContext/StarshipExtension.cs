using MayTheFourth.Core.Entities;
using MayTheFourth.DataImporter.Services.Contexts.FilmContext;
using MayTheFourth.DataImporter.Services.Contexts.StarshipContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace MayTheFourth.Api.Extensions.Contexts.StartshipContext;

public static class StarshipExtension
{
    public static void AddStarshipContext(this WebApplicationBuilder builder)
    {
        #region Register Starship Repository
        builder.Services.AddTransient<
            Core.Interfaces.Repositories.IStarshipRepository,
            Infra.Repositories.StarshipRepository>();
        #endregion

        #region Register Starship Import Service
        builder.Services.AddTransient<StarshipImportService>();
        #endregion
    }

    public static void MapStarshipEndpoints(this WebApplication app)
    {
        #region Get all starships
        app.MapGet("api/v1/starships", async (
            IRequestHandler<
                Core.Contexts.StarshipContext.UseCases.SearchAll.Request,
                Core.Contexts.StarshipContext.UseCases.SearchAll.Response> handler,
            [FromQuery] int pageNumber = 1, int pageSize = 10) =>
        {
            var request = new Core.Contexts.StarshipContext.UseCases.SearchAll.Request(pageNumber, pageSize);
            var result = await handler.Handle(request, new CancellationToken());

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        })
            .WithTags("Starship")
            .Produces(TypedResults.Ok().StatusCode)
            .Produces(TypedResults.BadRequest().StatusCode)
            .Produces(TypedResults.NotFound().StatusCode)
            .WithSummary("Return a list of starships")
            .WithOpenApi();
        #endregion

        #region Get starship by id
        app.MapGet("api/v1/starships/{id}", async (
            [FromRoute] Guid id,
            [FromServices] IRequestHandler<
                Core.Contexts.StarshipContext.UseCases.SearchById.Request,
                Core.Contexts.StarshipContext.UseCases.SearchById.Response> handler) =>
        {
            var request = new Core.Contexts.StarshipContext.UseCases.SearchById.Request(id);
            var result = await handler.Handle(request, new CancellationToken());

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        })
            .WithTags("Starship")
            .Produces(TypedResults.Ok().StatusCode)
            .Produces(TypedResults.BadRequest().StatusCode)
            .Produces(TypedResults.NotFound().StatusCode)
            .WithSummary("Return a starship according to ID")
            .WithOpenApi( opt =>
            {
                var parameter = opt.Parameters[0];
                parameter.Description = "The ID associated with the created Starship";
                return opt;
            });
        #endregion

        #region Get starship by slug
        app.MapGet("api/v1/starships/slug/{slug}", async (
            [FromRoute] string slug,
            [FromServices] IRequestHandler<
                Core.Contexts.StarshipContext.UseCases.SearchBySlug.Request,
                Core.Contexts.StarshipContext.UseCases.SearchBySlug.Response> handler) =>
        {
            var request = new Core.Contexts.StarshipContext.UseCases.SearchBySlug.Request(slug);
            var result = await handler.Handle(request, new CancellationToken());

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        })
            .WithTags("Starship")
            .Produces(TypedResults.Ok().StatusCode)
            .Produces(TypedResults.BadRequest().StatusCode)
            .Produces(TypedResults.NotFound().StatusCode)
            .WithSummary("Return a starship according to slug")
            .WithOpenApi();
        #endregion
    }
}
