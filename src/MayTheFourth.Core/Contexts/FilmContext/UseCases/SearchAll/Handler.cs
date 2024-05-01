using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Dtos;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MediatR;
using System.Net;
using System.Numerics;

namespace MayTheFourth.Core.Contexts.FilmContext.UseCases.SearchAll;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IFilmRepository _filmRepository;

    public Handler(IFilmRepository filmRepository)
        => _filmRepository = filmRepository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region GetAllFilms
        PagedList<Film>? films;
        int countItems = 0;
        int pageSizeLimit = 30;
        try
        {
            if (request.PageSize > pageSizeLimit)
                request.ChangePageSize(pageSizeLimit);

            countItems = await _filmRepository.CountItemsAsync();
            films = await _filmRepository.GetAllAsync(request.PageNumber, request.PageSize);

            if (films!.Count <= 0)
                return new Response("Nenhum filme encontrado.", ((int)HttpStatusCode.OK));

            if (request.PageSize > films.Count)
                films.ChangePageSize(countItems);
        }
        catch (Exception ex)
        {
            return new Response($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
        }

        List<FilmSummaryDto> filmSummaryList = films.Items!.Select(film => new FilmSummaryDto(film)).ToList();

        PagedList<FilmSummaryDto> filmPagedSummaryList =
            new(films.PageNumber, films.PageSize, countItems, filmSummaryList);

        var requestPageNumberOutOfRange =
            filmPagedSummaryList.PageNumber > Math.Ceiling((double)filmPagedSummaryList.Count / filmPagedSummaryList.PageSize);

        if (requestPageNumberOutOfRange)
            return new Response($"Número de página inválido.", (int)HttpStatusCode.BadRequest);
        #endregion

        #region Response
        return new Response("Lista de filmes encontrada", new ResponseData(filmPagedSummaryList));
        #endregion
    }
}
