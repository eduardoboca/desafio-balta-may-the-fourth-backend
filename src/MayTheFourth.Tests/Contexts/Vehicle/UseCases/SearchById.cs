using MayTheFourth.Core.Contexts.VehicleContext.UseCases.SearchById;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Tests.Repositories;
using System.Net;

namespace MayTheFourth.Tests.Contexts.Vehicle.UseCases;

[TestClass]
public class SearchById
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly Handler _handler;

    public SearchById()
    {
        _vehicleRepository = new FakeVehicleRepository();
        _handler = new(_vehicleRepository);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Return_404_When_Vehicle_Not_Found()
    {
        var guid = Guid.NewGuid();
        var request = new Request(guid);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual(((int)HttpStatusCode.NotFound), response.Status);
        Assert.AreEqual(false, response.IsSuccess);
    }

    [TestMethod]
    [TestCategory("Handler")]
    public async Task Should_Succeed_When_Vehicle_Found()
    {
        var guid = new Guid("378e6610-0b13-400c-96c0-b17a3055f14b");
        var request = new Request(guid);

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.AreEqual((((int)HttpStatusCode.OK)), response.Status);
        Assert.AreEqual(true, response.IsSuccess);
    }
}
