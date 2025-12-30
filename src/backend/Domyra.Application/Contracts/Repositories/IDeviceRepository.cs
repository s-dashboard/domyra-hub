using Domyra.Domain.Entities;

namespace Domyra.Application.Contracts.Repositories;

public interface IDeviceRepository
{
    Task<Device?> FetchSingleAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Device>> FetchAllAsync(string? filter, CancellationToken cancellationToken);
    Task AddAsync(Device device, CancellationToken cancellationToken);
    Task CommitChangesAsync(CancellationToken cancellationToken);
}