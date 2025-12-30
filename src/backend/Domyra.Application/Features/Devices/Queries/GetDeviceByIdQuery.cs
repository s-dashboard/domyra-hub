namespace Domyra.Application.Features.Devices.Queries;

using Models;
using MediatR;

public sealed record GetDeviceByIdQuery(int DeviceId)
    : IRequest<DeviceModel>;