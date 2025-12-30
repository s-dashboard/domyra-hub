using Domyra.Application.Contracts.Repositories;
using Domyra.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domyra.Infrastructure.Persistence.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly DomyraContext _context;
    
    public DeviceRepository(DomyraContext context)
    {
        _context = context;    
    }

    public async Task<Device?> FetchSingleAsync(int id, CancellationToken cancellationToken) =>
        await _context.Devices.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IEnumerable<Device>> FetchAllAsync(string? filter, CancellationToken cancellationToken)
    {
        var deviceQuery = _context.Devices.AsQueryable();
        if (string.IsNullOrEmpty(filter))
        {
            return await deviceQuery.ToListAsync(cancellationToken);
        }
        
        var term = filter.ToLower();
        
        deviceQuery = deviceQuery.Where(p =>
            p.Name.ToLower(
                ).Contains(term) || (p.Description != null && p.Description.ToLower().Contains(term))
        );
        
        return await deviceQuery.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Device device, CancellationToken cancellationToken) =>
        await _context.Devices.AddAsync(device, cancellationToken);

    public async Task CommitChangesAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);
}