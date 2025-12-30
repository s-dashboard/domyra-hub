namespace Domyra.Application.Features.Devices.Commands;

using Contracts.Services;
using MediatR;

public class ActivateDeviceCommandHandler : IRequestHandler<ActivateDeviceCommand>
{
    private readonly IDeviceService _deviceService;
    
    public ActivateDeviceCommandHandler(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }
    
    public async Task Handle(ActivateDeviceCommand request, CancellationToken cancellationToken)
    {
        await _deviceService.ActivateAsync(request.DeviceId, cancellationToken);
    }
}