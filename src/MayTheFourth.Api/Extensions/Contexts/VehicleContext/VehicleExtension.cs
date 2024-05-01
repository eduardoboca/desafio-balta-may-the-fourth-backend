using MayTheFourth.DataImporter.Services.Contexts.FilmContext;
using MayTheFourth.DataImporter.Services.Contexts.VehicleContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MayTheFourth.Api.Extensions.Contexts.VehicleContext;

public static class VehicleExtension
{
    public static void AddVehicleContext(this WebApplicationBuilder builder)
    {
        #region Register Vehicle Repository
        builder.Services.AddTransient
            <Core.Interfaces.Repositories.IVehicleRepository,
            Infra.Repositories.VehicleRepository>();
        #endregion

        #region Register Vehicle Import Service
        builder.Services.AddTransient<VehicleImportService>();
        #endregion
    }

    public static void MapVehicleEndpoints(this WebApplication app)
    {
        #region Get all vehicles
        app.MapGet("api/v1/vehicles", async
            (IRequestHandler<Core.Contexts.VehicleContext.UseCases.SearchAll.Request,
                Core.Contexts.VehicleContext.UseCases.SearchAll.Response> handler,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10) =>
        {
            var request = new Core.Contexts.VehicleContext.UseCases.SearchAll.Request(pageNumber, pageSize);
            var result = await handler.Handle(request, new CancellationToken());

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        })
            .WithTags("Vehicle")
            .Produces(TypedResults.Ok().StatusCode)
            .Produces(TypedResults.BadRequest().StatusCode)
            .Produces(TypedResults.NotFound().StatusCode)
            .WithSummary("Return a list of vehicles")
            .WithOpenApi();
        #endregion

        #region Get vehicle by id
        app.MapGet("api/v1/vehicles/{id:guid}", async (
            [FromRoute] Guid id,
            [FromServices] IRequestHandler<
                Core.Contexts.VehicleContext.UseCases.SearchById.Request,
                Core.Contexts.VehicleContext.UseCases.SearchById.Response> handler) =>
        {
            var request = new Core.Contexts.VehicleContext.UseCases.SearchById.Request(id);
            var result = await handler.Handle(request, new CancellationToken());

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        })
            .WithTags("Vehicle")
            .Produces(TypedResults.Ok().StatusCode)
            .Produces(TypedResults.BadRequest().StatusCode)
            .Produces(TypedResults.NotFound().StatusCode)
            .WithSummary("Return a vehicle according to ID")
            .WithOpenApi(opt =>
            {
                var parameter = opt.Parameters[0];
                parameter.Description = "The ID associated with the created Vehicle";
                return opt;
            });
        #endregion

        #region Get vehicle by slug
        app.MapGet("api/v1/vehicles/slug/{slug}", async (
            [FromRoute] string slug,
            [FromServices] IRequestHandler<
                Core.Contexts.VehicleContext.UseCases.SearchBySlug.Request,
                Core.Contexts.VehicleContext.UseCases.SearchBySlug.Response> handler) =>
        {
            var request = new Core.Contexts.VehicleContext.UseCases.SearchBySlug.Request(slug);
            var result = await handler.Handle(request, new CancellationToken());

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        })
            .WithTags("Vehicle")
            .Produces(TypedResults.Ok().StatusCode)
            .Produces(TypedResults.BadRequest().StatusCode)
            .Produces(TypedResults.NotFound().StatusCode)
            .WithSummary("Return a vehicle according to slug")
            .WithOpenApi();
        #endregion
    }
}