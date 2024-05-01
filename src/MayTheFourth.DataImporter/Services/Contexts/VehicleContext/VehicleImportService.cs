using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.DataImporter.DTOs;
using MayTheFourth.DataImporter.Services.Contexts.SharedContext;
using System.Text.Json;

namespace MayTheFourth.DataImporter.Services.Contexts.VehicleContext
{
    public class VehicleImportService : IImportService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private List<VehicleDTO> _vehicles = new();
        public VehicleImportService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public void Add(VehicleDTO vehicle) => _vehicles.Add(vehicle);

        public void LoadList(string jsonList)
            => _vehicles = JsonSerializer.Deserialize<List<VehicleDTO>>(jsonList)!;

        public async Task ImportAsync(CancellationToken cancellationToken)
        {
            foreach (var vehicleDTO in _vehicles)
            {
                await _vehicleRepository.SaveAsync(vehicleDTO.ToVehicle(), cancellationToken);
            }
        }

        public async Task<bool> IsEmpty() => !await _vehicleRepository.AnyAsync();
    }
}
