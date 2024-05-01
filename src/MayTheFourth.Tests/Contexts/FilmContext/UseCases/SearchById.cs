using MayTheFourth.Core.Contexts.FilmContext.UseCases.SearchById;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.FilmContext.UseCases;

[TestClass]
public class SearchById
{
    private readonly IFilmRepository _filmRepository;
    private readonly Handler _handler;

    public SearchById()
    {
        _filmRepository = new FakeFilmRepository();
        _handler = new(_filmRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_400_When_Film_Not_Found()
    {
        var guid = Guid.NewGuid();
        var request = new Request(guid);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.NotFound), response.Status);
        Assert.AreEqual(false, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Succeed_When_Film_Found()
    {
        var guid = new Guid("bb20333b-182c-4d5f-9a53-b7bd9c149639");
        var request = new Request(guid);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual((((int)HttpStatusCode.OK)), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }
}
