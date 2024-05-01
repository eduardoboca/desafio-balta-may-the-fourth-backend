using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Dtos;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace MayTheFourth.Core.Contexts.PlanetContext.UseCases.SearchAll;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IPlanetRepository _planetRepository;
    public Handler(IPlanetRepository planetRepository)
        => _planetRepository = planetRepository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region Get All Planets
        PagedList<Planet>? planets;
        int pageSizeLimit = 30;
        int countItems = 0;

        try
        {
            if (request.PageSize > pageSizeLimit)
                request.ChangePageSize(pageSizeLimit);

            countItems = await _planetRepository.CountTotalItemsAsync();
            planets = await _planetRepository.GetAllAsync(request.PageNumber, request.PageSize);

            if (planets!.Count <= 0)
                return new Response("Nenhum planeta encontrado.", ((int)HttpStatusCode.OK));

            if (request.PageSize > planets.Count)
                planets.ChangePageSize(countItems);
        }
        catch (Exception ex)
        {
            return new Response($"Erro: {ex.Message}", ((int)HttpStatusCode.InternalServerError));
        }

        List<PlanetSummaryDto> planetSummaryList = planets.Items!
            .Select(planet => new PlanetSummaryDto(planet)).ToList();

        PagedList<PlanetSummaryDto> planetPagedSummaryList = 
            new(planets.PageNumber, planets.PageSize, countItems, planetSummaryList);

        var requestPageNumberOutOfRange = 
            planetPagedSummaryList.PageNumber > Math.Ceiling((double)planetPagedSummaryList.Count / planetPagedSummaryList.PageSize);

        if (requestPageNumberOutOfRange)
            return new Response($"Erro: Número de páginas inválido.", ((int)HttpStatusCode.BadRequest));

        #endregion

        #region Response
        return new Response("Lista de planetas foi encontrado.", new ResponseData(planetPagedSummaryList));
        #endregion
    }
}
