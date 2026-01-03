using Domyra.Application.Contracts.Repositories;
using Domyra.Application.Exceptions;
using Domyra.Application.Features.Devices.Models;
using Domyra.Domain.Entities;
using MediatR;

namespace Domyra.Application.Features.Devices.Commands;

public class SaveDeviceCommandHandler : IRequestHandler<SaveDeviceCommand, DeviceModel>
{
    private readonly IDeviceRepository _deviceRepository;

    public SaveDeviceCommandHandler(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    public async Task<DeviceModel> Handle(SaveDeviceCommand request, CancellationToken cancellationToken)
    {
        Device device;

        if (request.Id is null || request.Id.Value == 0)
        {
            // Create device
            device = new Device()
            {
                Name = request.Name,
                Description = request.Description,
                State = request.State,
            };
            
            await _deviceRepository.AddAsync(device, cancellationToken); 
        }
        else
        {
            // Update device
            device = await _deviceRepository.FetchSingleAsync(request.Id.Value, cancellationToken)
                     ?? throw new DomyraNotFoundException(nameof(Device), request.Id);
            
            device.Name = request.Name;
            device.Description =  request.Description;
            device.State = request.State;
            
            _deviceRepository.MarkAsChanged(device);
        }

        await _deviceRepository.CommitChangesAsync(cancellationToken);

        return DeviceModel.From(device);
    }
}