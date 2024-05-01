using MayTheFourth.Core.Contexts.PersonContext.UseCases.SearchAll;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.PersonContext.UseCases;

[TestClass]
public class SearchAll
{
    private readonly IPersonRepository _personRepository;
    private readonly Handler _handler;

    public SearchAll()
    {
        _personRepository = new FakePersonRepository();
        _handler = new(_personRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_200_When_Repository_Returns_Empty_List()
    {
        var personRepository = new FakePersonRepository();
        personRepository.people.Clear();

        var pageNumber = 1;
        var pageSize = 10;
        var handler = new Handler(personRepository);

        var request = new Request(pageNumber, pageSize);

        var response = await handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.OK), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Succeed_When_PeopleList_Contains_Exactly_Five_People()
    {
        var pageNumber = 1;
        var pageSize = 10;
        var request = new Request(pageNumber, pageSize);

        var response = await _handler.Handle(request, new CancellationToken());

        Assert.AreEqual(5, response.Data!.PagedSummaryPeople.Count, "Expected exactly five person in the list.");
        Assert.AreEqual(true, response.IsSuccess);
        Assert.AreEqual(((int)HttpStatusCode.OK), response.Status);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_Error_When_PageNumber_Request_Is_Greater_Than_Total_Pages_Available()
    {
        var pageNumber = 100;
        var pageSize = 10;
        var handler = new Handler(_personRepository);

        var request = new Request(pageNumber, pageSize);

        var response = await handler.Handle(request, new CancellationToken());

        Assert.AreEqual(false, response.IsSuccess);
        Assert.AreEqual(((int)HttpStatusCode.BadRequest), response.Status);
    }

}
