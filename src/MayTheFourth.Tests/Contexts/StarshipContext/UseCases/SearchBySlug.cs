using MayTheFourth.Core.Contexts.StarshipContext.UseCases.SearchBySlug;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.StarshipContext.UseCases;

public class SearchBySlug
{
    private readonly IStarshipRepository _starshipRepository;
    private readonly Handler _handler;

    public SearchBySlug()
    {
        _starshipRepository = new FakeStarshipRepository();
        _handler = new(_starshipRepository);
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
        var slug = "x-wing";
        var request = new Request(slug);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual((((int)HttpStatusCode.OK)), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }
}
