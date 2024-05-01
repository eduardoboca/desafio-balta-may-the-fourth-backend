using MayTheFourth.Core.Contexts.SpeciesContext.UseCases.SearchAll;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.SpecieContext.UseCases;

[TestClass]
public class SearchAll
{
    private readonly ISpeciesRepository _specieRepository;
    private readonly Handler _handler;

    public SearchAll()
    {
        _specieRepository = new FakeSpecieRepository();
        _handler = new(_specieRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_200_When_Repository_Returns_Empty_List()
    {
        var specieRepository = new FakeSpecieRepository();
        specieRepository.species.Clear();

        var pageNumber = 1;
        var pageSize = 10;
        var handler = new Handler(specieRepository);
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
    public async Task Should_Succeed_When_SpecieList_Contains_Exactly_Five_Species()
    {
        var pageNumber = 1;
        var pageSize = 10;
        var request = new Request(pageNumber, pageSize);

        var response = await _handler.Handle(request, new CancellationToken());

        Assert.AreEqual(5, response.Data!.Species.Count, "Expected exactly five species in the list.");
        Assert.AreEqual(true, response.IsSuccess);
        Assert.AreEqual(((int)HttpStatusCode.OK), response.Status);
    }
}
