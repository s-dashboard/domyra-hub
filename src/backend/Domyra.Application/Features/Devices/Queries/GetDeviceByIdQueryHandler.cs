using Domyra.Application.Contracts.Repositories;
using Domyra.Application.Exceptions;
using Domyra.Domain.Entities;

namespace Domyra.Application.Features.Devices.Queries;

using Models;
using MediatR;

public sealed class GetDeviceByIdQueryHandler : 
    IRequestHandler<GetDeviceByIdQuery, DeviceModel>
{
    private readonly IDeviceRepository _deviceRepository;

    public GetDeviceByIdQueryHandler(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    public async Task<DeviceModel> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await _deviceRepository.FetchSingleAsync(request.DeviceId, cancellationToken)
                     ?? throw new DomyraNotFoundException(nameof(Device), request.DeviceId);
        
        return DeviceModel.From(device);
    }
}