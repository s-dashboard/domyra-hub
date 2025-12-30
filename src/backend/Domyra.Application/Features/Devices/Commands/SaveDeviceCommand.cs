using Domyra.Application.Features.Devices.Models;
using Domyra.Domain.Enums;
using MediatR;

namespace Domyra.Application.Features.Devices.Commands;

public sealed record SaveDeviceCommand(
    int? Id,
    string Name,
    string? Description,
    DeviceState State
) : IRequest<DeviceModel>;