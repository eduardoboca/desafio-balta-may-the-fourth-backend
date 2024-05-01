using MayTheFourth.Core.Contexts.FilmContext.UseCases.SearchBySlug;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.FilmContext.UseCases;

[TestClass]
public class SearchBySlug
{
    private readonly IFilmRepository _filmRepository;
    private readonly Handler _handler;

    public SearchBySlug()
    {
        _filmRepository = new FakeFilmRepository();
        _handler = new(_filmRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_404_When_Film_Not_Found()
    {
        var slug = "NewSlug";
        var request = new Request(slug);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.NotFound), response.Status);
        Assert.AreEqual(false, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Succeed_When_Film_Found()
    {
        var slug = "filme-01";
        var request = new Request(slug);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual((((int)HttpStatusCode.OK)), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }
}
