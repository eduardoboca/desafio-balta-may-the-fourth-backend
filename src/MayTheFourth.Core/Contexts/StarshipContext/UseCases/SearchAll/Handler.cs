using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Dtos;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace MayTheFourth.Core.Contexts.StarshipContext.UseCases.SearchAll;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IStarshipRepository _starshipRepository;

    public Handler(IStarshipRepository starshipRepository)
        => _starshipRepository = starshipRepository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region Get List of Starships
        PagedList<Starship>? starships;
        int pageSizeLimit = 30;
        int countItems = 0;

        try
        {
            if (request.PageSize > pageSizeLimit)
                request.ChangePageSize(pageSizeLimit);

            countItems = await _starshipRepository.CountTotalItemsAsync();
            starships = await _starshipRepository.GetAllAsync(request.PageNumber, request.PageSize);

            if (starships!.Count <= 0)
                return new Response("Nenhuma nave encontrada.", ((int)HttpStatusCode.OK));

            if (request.PageSize > starships.Count)
                starships.ChangePageSize(countItems);
        }
        catch (Exception ex)
        {
            return new Response($"Erro: {ex.Message}", ((int)HttpStatusCode.InternalServerError));
        }

        List<StarshipSummaryDto> starshipSummaryList = starships.Items!
            .Select(starship => new StarshipSummaryDto(starship)).ToList();

        PagedList<StarshipSummaryDto> starshipPagedSummary =
            new(starships.PageNumber, starships.PageSize, countItems, starshipSummaryList);

        var requestPageNumberOutOfRange =
            starshipPagedSummary.PageNumber > Math.Ceiling((double)starshipPagedSummary.Count / starshipPagedSummary.PageSize);

        if (requestPageNumberOutOfRange)
            return new Response($"Erro: Número de páginas inválido.", ((int)HttpStatusCode.BadRequest));
        #endregion

        #region Response
        return new Response("Lista de naves encontrada.", new ResponseData(starshipPagedSummary));
        #endregion
    }
}
