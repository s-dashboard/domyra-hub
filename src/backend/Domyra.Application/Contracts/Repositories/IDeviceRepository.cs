using Domyra.Domain.Entities;

namespace Domyra.Application.Contracts.Repositories;

public interface IDeviceRepository
    : IBaseRepository<Device>
{
    Task<Device?> FetchSingleAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Device>> FetchAllAsync(string? filter, CancellationToken cancellationToken);
}