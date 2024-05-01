using MayTheFourth.Core.Contexts.VehicleContext.UseCases.SearchBySlug;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.Vehicle.UseCases;

[TestClass]
public class SearchBySlug
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly Handler _handler;

    public SearchBySlug()
    {
        _vehicleRepository = new FakeVehicleRepository();
        _handler = new(_vehicleRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_Error_404_When_Vehicle_Not_Found()
    {
        var slug = "NewSlug";
        var request = new Request(slug);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.NotFound), response.Status);
        Assert.AreEqual(false, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Succeed_When_Vehicle_Found()
    {
        var slug = "snowspeeder";
        var request = new Request(slug);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual((((int)HttpStatusCode.OK)), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }
}
