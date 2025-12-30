namespace Domyra.Application.Features.Devices.Queries;

using Contracts.Repositories;
using Models;
using MediatR;

public sealed class GetDevicesQueryHandler :
    IRequestHandler<GetDevicesQuery, IEnumerable<DeviceModel>>
{
    private readonly IDeviceRepository _deviceRepository;
    
    public GetDevicesQueryHandler(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    public async Task<IEnumerable<DeviceModel>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
    { 
        var devices = await _deviceRepository.FetchAllAsync(request.Filter, cancellationToken);
        
        return devices.Select(DeviceModel.From);
    }
}