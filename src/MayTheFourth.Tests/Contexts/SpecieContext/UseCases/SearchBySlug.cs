using MayTheFourth.Core.Contexts.SpeciesContext.UseCases.SearchBySlug;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.SpecieContext.UseCases;

[TestClass]
public class SearchBySlug
{
    private readonly ISpeciesRepository _specieRepository;
    private readonly Handler _handler;

    public SearchBySlug()
    {
        _specieRepository = new FakeSpecieRepository();
        _handler = new(_specieRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_Error_404_When_Specie_Not_Found()
    {
        var slug = "NewSlug";
        var request = new Request(slug);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.NotFound), response.Status);
        Assert.AreEqual(false, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Succeed_When_Specie_Found()
    {
        var slug = "droid";
        var request = new Request(slug);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual((((int)HttpStatusCode.OK)), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }
}
