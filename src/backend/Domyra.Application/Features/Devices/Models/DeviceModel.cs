using Domyra.Domain.Entities;
using Domyra.Domain.Enums;

namespace Domyra.Application.Features.Devices.Models;

public sealed record DeviceModel(
    int Id,
    string Name,
    string? Description,
    DeviceState State)
{
    public static DeviceModel From(Device device) =>
        new(
            device.Id,
            device.Name,
            device.Description,
            device.State
        );
}
