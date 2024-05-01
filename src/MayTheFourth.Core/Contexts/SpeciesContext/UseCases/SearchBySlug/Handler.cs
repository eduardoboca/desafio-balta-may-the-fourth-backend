using MayTheFourth.Core.Dtos;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace MayTheFourth.Core.Contexts.SpeciesContext.UseCases.SearchBySlug;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly ISpeciesRepository _speciesRepository;
    public Handler(ISpeciesRepository speciesRepository)
        => _speciesRepository = speciesRepository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region Get Species by slug
        Species? species;

        try
        {
            species = await _speciesRepository.GetBySlugAsync(request.Slug, cancellationToken);
            if (species == null)
                return new Response("Erro: Espécie não encontrada.", ((int)HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            return new Response($"Erro: {ex.Message}", ((int)HttpStatusCode.InternalServerError));
        }

        SpeciesDetailsDto speciesDetails = new(species);
        #endregion

        #region Response
        return new Response("Espécie encontrada com sucesso.", new ResponseData(speciesDetails));
        #endregion
    }
}
