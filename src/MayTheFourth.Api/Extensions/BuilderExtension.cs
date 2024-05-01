using MayTheFourth.Core;
using MayTheFourth.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MayTheFourth.Api.Extensions;

public static class BuilderExtension
{

    public static void AddConfiguration(this WebApplicationBuilder builder)
        => Configuration.Database.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

    public static void AddMediatR(this WebApplicationBuilder builder)
    => builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssembly(typeof(Configuration).Assembly));

    public static void AddDbContext(this WebApplicationBuilder builder)
        => builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.Database.ConnectionString, assembly => assembly.MigrationsAssembly("MayTheFourth.Api")));

    public static void AddCorsConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("RebelRenegades", builder =>
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
    }

    public static void AddSwaggerConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MayTheFourth API - Rebel Renegades",
                Version = "v1",
                Description = "API para consulta de dados do universo de Star Wars",
                Contact = new OpenApiContact
                {
                    Name = "Capitão Igor",
                    Url = new Uri("https://github.com/igorsantiiago/desafio-balta-may-the-fourth-backend")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License"
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            options.IncludeXmlComments(xmlPath);
        });

        builder.Services.ConfigureSwaggerGen(options => options.CustomSchemaIds(x => x.FullName));
    }
}
