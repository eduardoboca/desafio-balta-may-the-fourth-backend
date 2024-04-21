﻿using MediatR;

namespace MayTheFourth.Api.Extensions.Contexts.FilmContext
{
    public static class FilmExtension
    {
        public static void AddFilmContext (this WebApplicationBuilder builder)
        {
            #region Register Film Repository
            builder.Services.AddTransient
                <Core.Interfaces.Repositories.IFilmRepository,
                Infra.Repositories.FilmRepository>();
            #endregion
        }

        public static void MapFilmEndpoints(this WebApplication app)
        {
            #region Get all films
            app.MapGet("api/v1/film", async
                (IRequestHandler<Core.Contexts.FilmContext.UseCases.SearchAll.Request,
                Core.Contexts.FilmContext.UseCases.SearchAll.Response> handler) =>
            {
                var request = new Core.Contexts.FilmContext.UseCases.SearchAll.Request();
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.Status);
            });
            #endregion
        }

    }
}