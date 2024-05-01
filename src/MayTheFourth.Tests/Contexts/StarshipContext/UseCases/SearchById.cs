using MayTheFourth.Core.Contexts.StarshipContext.UseCases.SearchById;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.StarshipContext.UseCases;

[TestClass]
public class SearchById
{
    private readonly IStarshipRepository _starshipRepository;
    private readonly Handler _handler;

    public SearchById()
    {
        _starshipRepository = new FakeStarshipRepository();
        _handler = new(_starshipRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_Error_404_When_Starship_Not_Found()
    {
        var guid = Guid.NewGuid();
        var request = new Request(guid);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.NotFound), response.Status);
        Assert.AreEqual(false, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Succeed_When_Starship_Found()
    {
        var guid = new Guid("1ca12345-6789-0abc-def0-1234567890ab");
        var request = new Request(guid);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual((((int)HttpStatusCode.OK)), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }
}
