using MayTheFourth.Core.Contexts.PersonContext.UseCases.SearchBySlug;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.PersonContext.UseCases;

[TestClass]
public class SearchBySlug
{
    private readonly IPersonRepository _personRepository;
    private readonly Handler _handler;

    public SearchBySlug()
    {
        _personRepository = new FakePersonRepository();
        _handler = new(_personRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_Error_404_When_Person_Not_Found()
    {
        var slug = "NewSlug";
        var request = new Request(slug);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.NotFound), response.Status);
        Assert.AreEqual(false, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Succeed_When_Person_Found()
    {
        var slug = "Yoda";
        var request = new Request(slug);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual((((int)HttpStatusCode.OK)), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }
}
