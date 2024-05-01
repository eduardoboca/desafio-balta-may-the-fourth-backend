using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Dtos;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace MayTheFourth.Core.Contexts.SpeciesContext.UseCases.SearchAll;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly ISpeciesRepository _speciesRepository;
    public Handler(ISpeciesRepository speciesRepository)
        => _speciesRepository = speciesRepository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region GetAllSpecies
        PagedList<Species>? species;
        int countItems = 0;
        int pageSizeLimit = 30;

        try
        {
            if (request.PageSize > pageSizeLimit)
                request.ChangePageSize(pageSizeLimit);

            countItems = await _speciesRepository.CountItemsAsync();
            species = await _speciesRepository.GetAllAsync(request.PageNumber, request.PageSize);

            if (species.Count <= 0)
                return new Response("Nenhuma espécie encontrada.", ((int)HttpStatusCode.OK));

            if (request.PageSize > species.Count)
                species.ChangePageSize(countItems);
        }
        catch (Exception ex)
        {
            return new Response($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
        }

        List<SpeciesSummaryDto> speciesSummaryList = species.Items!.Select(species => new SpeciesSummaryDto(species)).ToList();

        PagedList<SpeciesSummaryDto> speciesPagedSummaryList =
            new(species.PageNumber, species.PageSize, countItems, speciesSummaryList);

        var requestPageNumberOutOfRange =
            speciesPagedSummaryList.PageNumber > Math.Ceiling((double)speciesPagedSummaryList.Count / speciesPagedSummaryList.PageSize);

        if (requestPageNumberOutOfRange)
            return new Response($"Erro: Número de página inválido.", (int)HttpStatusCode.BadRequest);
        #endregion

        #region Response
        return new Response("Lista de espécies encontrada", new ResponseData(speciesPagedSummaryList));
        #endregion
    }
}
