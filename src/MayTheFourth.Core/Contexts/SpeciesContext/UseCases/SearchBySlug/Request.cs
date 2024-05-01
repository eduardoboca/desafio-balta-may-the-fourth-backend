using MediatR;

namespace MayTheFourth.Core.Contexts.SpeciesContext.UseCases.SearchBySlug;

public record Request(string Slug) : IRequest<Response>;
