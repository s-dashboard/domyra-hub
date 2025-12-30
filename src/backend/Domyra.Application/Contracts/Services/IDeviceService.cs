namespace Domyra.Application.Contracts.Services;

public interface IDeviceService
{
    Task ActivateAsync(int deviceId, CancellationToken cancellationToken);
}