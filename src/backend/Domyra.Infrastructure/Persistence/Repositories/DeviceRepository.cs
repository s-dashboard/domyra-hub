using Domyra.Application.Contracts.Repositories;
using Domyra.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domyra.Infrastructure.Persistence.Repositories;

public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
{
    public DeviceRepository(DomyraContext context) : base(context)
    {
    }

    public async Task<Device?> FetchSingleAsync(int id, CancellationToken cancellationToken) =>
        await Context.Devices.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IEnumerable<Device>> FetchAllAsync(string? filter, CancellationToken cancellationToken)
    {
        var deviceQuery = Context.Devices.AsQueryable();
        if (string.IsNullOrEmpty(filter))
        {
            return await deviceQuery.OrderBy(d => d.Id)
                .ToListAsync(cancellationToken);
        }
        
        var term = filter.ToLower();
        
        deviceQuery = deviceQuery.Where(p =>
            p.Name.ToLower(
                ).Contains(term) || (p.Description != null && p.Description.ToLower().Contains(term))
        );
        
        return await deviceQuery.OrderBy(d => d.Id)
            .ToListAsync(cancellationToken);
    }
}