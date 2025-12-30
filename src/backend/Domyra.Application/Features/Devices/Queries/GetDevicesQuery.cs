using Domyra.Application.Features.Devices.Models;

namespace Domyra.Application.Features.Devices.Queries;

using MediatR;

public sealed record GetDevicesQuery(string Filter) : IRequest<IEnumerable<DeviceModel>>;