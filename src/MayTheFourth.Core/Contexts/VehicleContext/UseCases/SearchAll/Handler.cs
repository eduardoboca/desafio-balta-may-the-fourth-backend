using System.Net;
using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Dtos;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MediatR;

namespace MayTheFourth.Core.Contexts.VehicleContext.UseCases.SearchAll;

public class Handler: IRequestHandler<Request, Response>
{
    private readonly IVehicleRepository _vehicleRepository;
    public Handler(IVehicleRepository vehicleRepository)
        => _vehicleRepository = vehicleRepository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region Get All Vehicles
        PagedList<Vehicle>? vehicles;
        int pageSizeLimit = 30;
        int countItems = 0;

        try
        {
            if(request.PageSize > pageSizeLimit)
                request.ChangePageSize(pageSizeLimit);

            countItems = await _vehicleRepository.CountTotalItemsAsync();
            vehicles = await _vehicleRepository.GetAllAsync(request.PageNumber, request.PageSize);

            if (vehicles is null)
                return new Response("Nenhum veículo encontrado.", (int)HttpStatusCode.OK);

            if (request.PageSize > vehicles.Count)
                vehicles.ChangePageSize(countItems);
        }
        catch (Exception ex)
        {
            return new Response($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
        }

        List<VehicleSummaryDto> vehicleSummaryList = vehicles.Items!
            .Select(vehicle => new VehicleSummaryDto(vehicle)).ToList();

        PagedList<VehicleSummaryDto> vehiclesPagedSummary =
            new(vehicles.PageNumber, vehicles.PageSize, countItems, vehicleSummaryList);

        var requestPageNumberOutOfRange =
            vehiclesPagedSummary.PageNumber > Math.Ceiling((double)vehiclesPagedSummary.Count / vehiclesPagedSummary.PageSize);

        if (requestPageNumberOutOfRange)
            return new Response($"Erro: Número de páginas inválido.", ((int)HttpStatusCode.BadRequest));
        #endregion

        #region Response
        return new Response("Lista de veículos encontrada.", new ResponseData(vehiclesPagedSummary));
        #endregion
    }
}