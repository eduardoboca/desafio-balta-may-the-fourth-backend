using MayTheFourth.Core.Contexts.SharedContext;
using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace MayTheFourth.Infra.Repositories;

public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(AppDbContext appDbContext) : base(appDbContext) { }

    public async Task<bool> AnyAsync()
        => await _appDbContext.Planets.AnyAsync();

    public async Task<bool> AnyAsync(string name, string model)
        => await _appDbContext.Vehicles.AnyAsync(x => x.Name == name && x.Model == model);
    
    public async Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _appDbContext.Vehicles
            .Include(x => x.Films)
            .Include(x => x.Pilots)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<Vehicle?> GetBySlugAsync(string slug, CancellationToken cancellationToken)  
        => await _appDbContext.Vehicles
            .Include(x => x.Films)
            .Include(x => x.Pilots)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);

    public async Task SaveAsync(Vehicle vehicle, CancellationToken cancellationToken)
    {
        await _appDbContext.Vehicles.AddAsync(vehicle, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteVehicleByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var vehicle = await _appDbContext.Vehicles.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
        if (vehicle is null) return false;
        _appDbContext.Vehicles.Remove(vehicle);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return true;

    }

    public async Task<int> CountTotalItemsAsync()
        => await _appDbContext.Vehicles.CountAsync();

    public async Task<PagedList<Vehicle>?> GetAllAsync(int pageNumber, int pageSize)
    {
        var vehicles = _appDbContext.Vehicles.AsQueryable();
        return await GetPagedAsync(vehicles, pageNumber, pageSize);
    }

    public async Task<Vehicle?> GetByUrlAsync(string url, CancellationToken cancellationToken)
    => await _appDbContext.Vehicles
        .FirstOrDefaultAsync(x => x.Url == url);

    public async Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken)
    {
        _appDbContext.Update(vehicle);
        await _appDbContext.SaveChangesAsync();
    }
}