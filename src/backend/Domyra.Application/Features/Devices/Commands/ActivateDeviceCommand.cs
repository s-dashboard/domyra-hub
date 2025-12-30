namespace Domyra.Application.Features.Devices.Commands;

using MediatR;

public sealed record ActivateDeviceCommand(int DeviceId) :
    IRequest; 