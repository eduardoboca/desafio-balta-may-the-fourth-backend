using MayTheFourth.Core.Contexts.PlanetContext.UseCases.SearchBySlug;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.PlanetContext.UseCases;

public class SearchBySlug
{
    private readonly IPlanetRepository _planetRepository;
    private readonly Handler _handler;

    public SearchBySlug()
    {
        _planetRepository = new FakePlanetRepository();
        _handler = new(_planetRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_Error_404_When_Planet_Not_Found()
    {
        var slug = "NewSlug";
        var request = new Request(slug);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.NotFound), response.Status);
        Assert.AreEqual(false, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Succeed_When_Planet_Found()
    {
        var slug = "tatooine";
        var request = new Request(slug);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual((((int)HttpStatusCode.OK)), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }
}
