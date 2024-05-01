using MayTheFourth.Core.Contexts.FilmContext.UseCases.SearchAll;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.FilmContext.UseCases;

[TestClass]
public class SearchAll
{
    private readonly IFilmRepository _filmRepository;
    private readonly Handler _handler;

    public SearchAll()
    {
        _filmRepository = new FakeFilmRepository();
        _handler = new(_filmRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_200_When_Repository_Returns_Empty_List()
    {
        var filmsRepository = new FakeFilmRepository();
        filmsRepository.films.Clear();

        var pageNumber = 1;
        var pageSize = 10;
        var handler = new Handler(filmsRepository);

        var request = new Request(pageNumber, pageSize);

        var response = await handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.OK), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_Exactly_Five_Films()
    {
        var pageNumber = 1;
        var pageSize = 10;
        var request = new Request(pageNumber, pageSize);

        var response = await _handler.Handle(request, new CancellationToken());

        Assert.AreEqual(5, response.Data!.PagedSummaryFilms.Count, "Expected exactly five films in the list.");
        Assert.AreEqual(true, response.IsSuccess);
        Assert.AreEqual(((int)HttpStatusCode.OK), response.Status);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_Error_When_PageNumber_Request_Is_Greater_Than_Total_Pages_Available()
    {
        var pageNumber = 100000;
        var pageSize = 10;
        var handler = new Handler(_filmRepository);

        var request = new Request(pageNumber, pageSize);

        var response = await handler.Handle(request, new CancellationToken());

        Assert.AreEqual(false, response.IsSuccess);
        Assert.AreEqual(((int)HttpStatusCode.BadRequest), response.Status);
    }
}
