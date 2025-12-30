using Domyra.Application.Exceptions;
using Domyra.Domain.Entities;
using Domyra.Domain.Enums;

namespace Domyra.Application.Features.Devices.Services;

using Domyra.Application.Contracts.Services;
using Contracts.Repositories;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;

    public DeviceService(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }
    
    public async Task ActivateAsync(int deviceId, CancellationToken cancellationToken)
    {
        var device = await _deviceRepository.FetchSingleAsync(deviceId, cancellationToken);
        
        if (device == null)
        {
            throw new DomyraNotFoundException(nameof(Device), deviceId);
        }
        
        device.State = device.State == DeviceState.Active ? DeviceState.Inactive : DeviceState.Active;
        _deviceRepository.MarkAsChanged(device);
        await _deviceRepository.CommitChangesAsync(cancellationToken);
    }
}