using MayTheFourth.Core.Contexts.PlanetContext.UseCases.SearchById;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.PlanetContext.UseCases;

[TestClass]
public class SearchById
{
    private readonly IPlanetRepository _planetRepository;
    private readonly Handler _handler;

    public SearchById()
    {
        _planetRepository = new FakePlanetRepository();
        _handler = new(_planetRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_Error_404_When_Planet_Not_Found()
    {
        var guid = Guid.NewGuid();
        var request = new Request(guid);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.NotFound), response.Status);
        Assert.AreEqual(false, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Succeed_When_Planet_Found()
    {
        var guid = new Guid("b32f441e-2f1e-421b-a484-2e1d1de32f1b");
        var request = new Request(guid);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual((((int)HttpStatusCode.OK)), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }
}
