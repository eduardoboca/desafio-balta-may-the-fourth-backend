using MayTheFourth.DataImporter.Services.Contexts.PlanetContext;
using MayTheFourth.DataImporter.Services.Contexts.SharedContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MayTheFourth.Api.Extensions.Contexts.PlanetContext;

public static class PlanetExtension
{
    public static void AddPlanetContext(this WebApplicationBuilder builder)
    {
        #region Register Planet Repository
        builder.Services.AddTransient
            <Core.Interfaces.Repositories.IPlanetRepository,
            Infra.Repositories.PlanetRepository>();
        #endregion

        #region Register Planet Import Service
        builder.Services.AddTransient <PlanetImportService>();
        #endregion
    }

    public static void MapPlanetEndpoints(this WebApplication app)
    {
        #region Get all planets
        app.MapGet("api/v1/planets", async
            (IRequestHandler
                <Core.Contexts.PlanetContext.UseCases.SearchAll.Request,
                Core.Contexts.PlanetContext.UseCases.SearchAll.Response> handler, 
            [FromQuery] int pageNumber = 1, int pageSize = 10) =>
        {
            var request = new Core.Contexts.PlanetContext.UseCases.SearchAll.Request(pageNumber, pageSize);
            var result = await handler.Handle(request, new CancellationToken());

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        })
            .WithTags("Planet")
            .Produces(TypedResults.Ok().StatusCode)
            .Produces(TypedResults.NotFound().StatusCode)
            .Produces(TypedResults.BadRequest().StatusCode)
            .WithSummary("Return a list of planets")
            .WithOpenApi();
        #endregion

        #region Get planet by id
        app.MapGet("api/v1/planets/{id}", async (
            [FromRoute] Guid id,
            [FromServices] IRequestHandler<
                Core.Contexts.PlanetContext.UseCases.SearchById.Request,
                Core.Contexts.PlanetContext.UseCases.SearchById.Response> handler) =>
        {
            var request = new Core.Contexts.PlanetContext.UseCases.SearchById.Request(id);
            var result = await handler.Handle(request, new CancellationToken());

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        })
            .WithTags("Planet")
            .Produces(TypedResults.Ok().StatusCode)
            .Produces(TypedResults.NotFound().StatusCode)
            .Produces(TypedResults.BadRequest().StatusCode)
            .WithSummary("Return a planet according to ID")
            .WithOpenApi(opt =>
            {
                var parameter = opt.Parameters[0];
                parameter.Description = "The ID associated with the created Planet";
                return opt;
            });
        #endregion

        #region Get planet by slug
        app.MapGet("api/v1/planets/slug/{slug}", async (
            [FromRoute] string slug,
            [FromServices] IRequestHandler<
                Core.Contexts.PlanetContext.UseCases.SearchBySlug.Request,
                Core.Contexts.PlanetContext.UseCases.SearchBySlug.Response> handler) =>
        {
            var request = new Core.Contexts.PlanetContext.UseCases.SearchBySlug.Request(slug);
            var result = await handler.Handle(request, new CancellationToken());

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        })
            .WithTags("Planet")
            .Produces(TypedResults.Ok().StatusCode)
            .Produces(TypedResults.NotFound().StatusCode)
            .Produces(TypedResults.BadRequest().StatusCode)
            .WithSummary("Return a planet according to slug")
            .WithOpenApi();
        #endregion
    }
}
