﻿using MayTheFourth.DataImporter.Services.Contexts.FilmContext;
using MayTheFourth.DataImporter.Services.Contexts.SpeciesContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MayTheFourth.Api.Extensions.Contexts.SpeciesContext
{
    public static class SpeciesExtension
    {
        public static void AddSpeciesContext(this WebApplicationBuilder builder)
        {
            #region Register Species Repository
            builder.Services.AddTransient
                <Core.Interfaces.Repositories.ISpeciesRepository,
                Infra.Repositories.SpeciesRepository>();
            #endregion

            #region Register Species Import Service
            builder.Services.AddTransient<SpeciesImportService>();
            #endregion
        }

        public static void MapSpeciesEndpoints(this WebApplication app)
        {
            #region Get all species
            app.MapGet("api/v1/species", async
                (IRequestHandler<Core.Contexts.SpeciesContext.UseCases.SearchAll.Request,
                Core.Contexts.SpeciesContext.UseCases.SearchAll.Response> handler,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10) =>
            {
                var request = new Core.Contexts.SpeciesContext.UseCases.SearchAll.Request(pageNumber, pageSize);
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.Status);
            })
                .WithTags("Specie")
                .Produces(TypedResults.Ok().StatusCode)
                .Produces(TypedResults.BadRequest().StatusCode)
                .Produces(TypedResults.NotFound().StatusCode)
                .WithSummary("Return a list of species")
                .WithOpenApi();
            #endregion

            #region Get species by id
            app.MapGet("api/v1/species/{id}", async (
                [FromRoute] Guid id,
                [FromServices] IRequestHandler<
                    Core.Contexts.SpeciesContext.UseCases.SearchById.Request,
                    Core.Contexts.SpeciesContext.UseCases.SearchById.Response> handler) =>
            {
                var request = new Core.Contexts.SpeciesContext.UseCases.SearchById.Request(id);
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.Status);
            })
                .WithTags("Specie")
                .Produces(TypedResults.Ok().StatusCode)
                .Produces(TypedResults.BadRequest().StatusCode)
                .Produces(TypedResults.NotFound().StatusCode)
                .WithSummary("Returns a species according to ID")
                .WithOpenApi(opt =>
                {
                    var parameter = opt.Parameters[0];
                    parameter.Description = "The ID associated with the created Species";
                    return opt;
                });
            #endregion

            #region Get species by slug
            app.MapGet("api/v1/species/slug/{slug}", async (
                [FromRoute] string slug,
                [FromServices] IRequestHandler<
                    Core.Contexts.SpeciesContext.UseCases.SearchBySlug.Request,
                    Core.Contexts.SpeciesContext.UseCases.SearchBySlug.Response> handler) =>
            {
                var request = new Core.Contexts.SpeciesContext.UseCases.SearchBySlug.Request(slug);
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.Status);
            })
                .WithTags("Specie")
                .Produces(TypedResults.Ok().StatusCode)
                .Produces(TypedResults.BadRequest().StatusCode)
                .Produces(TypedResults.NotFound().StatusCode)
                .WithSummary("Returns a species according to Slug")
                .WithOpenApi(opt =>
                {
                    var parameter = opt.Parameters[0];
                    parameter.Description = "The slug associated with the created Species";
                    return opt;
                });
            #endregion
        }

    }
}
