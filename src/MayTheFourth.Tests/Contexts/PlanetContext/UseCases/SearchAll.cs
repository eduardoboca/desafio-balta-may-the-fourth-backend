using MayTheFourth.Core.Contexts.PlanetContext.UseCases.SearchAll;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.PlanetContext.UseCases;

[TestClass]
public class SearchAll
{
    private readonly IPlanetRepository _planetRepository;
    private readonly Handler _handler;

    public SearchAll()
    {
        _planetRepository = new FakePlanetRepository();
        _handler = new(_planetRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_200_When_Repository_Returns_Empty_List()
    {
        var planetRepository = new FakePlanetRepository();
        planetRepository._planets.Clear();

        var pageNumber = 1;
        var pageSize = 10;
        var handler = new Handler(planetRepository);
        var request = new Request(pageNumber, pageSize);

        var response = await handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.OK), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_Error_When_PageNumber_Request_Is_Greater_Than_Total_Pages_Available()
    {
        var pageNumber = 100;
        var pageSize = 10;
        var request = new Request(pageNumber, pageSize);

        var response = await _handler.Handle(request, new CancellationToken());

        Assert.AreEqual(false, response.IsSuccess);
        Assert.AreEqual(((int)HttpStatusCode.BadRequest), response.Status);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Succeed_When_PlanetList_Contains_Exactly_Five_Planets()
    {
        var pageNumber = 1;
        var pageSize = 10;
        var request = new Request(pageNumber, pageSize);

        var response = await _handler.Handle(request, new CancellationToken());
        
        Assert.AreEqual(5, response.Data!.Planets.Count, "Expected exactly five planets in the list.");
        Assert.AreEqual(true, response.IsSuccess);
        Assert.AreEqual(((int)HttpStatusCode.OK), response.Status);
    }
}
