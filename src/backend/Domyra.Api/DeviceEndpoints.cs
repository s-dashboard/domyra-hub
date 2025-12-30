using Domyra.Application.Features.Devices.Commands;
using Domyra.Application.Features.Devices.Queries;
using MediatR;

namespace Domyra.Api;

public static class DeviceEndpoints
{
    public static IEndpointRouteBuilder MapDeviceEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/devices")
            .WithTags("Devices");
        
        group.MapGet("/", GetDevices);
        group.MapPost("/", CreateDevice);
        group.MapGet("/{id:int}", GetDeviceById);
        group.MapPut("/{id:int}", UpdateDevice);

        return endpoints;
    }
    
    private async static Task<IResult> GetDevices(string? filter, ISender sender)
    {
        var devices = await sender.Send(new GetDevicesQuery(filter ?? string.Empty));
        return Results.Ok(devices);
    }
    
    private async static Task<IResult> GetDeviceById(
        int id,
        ISender sender)
    {
        var device = await sender.Send(new GetDeviceByIdQuery(id));
        return Results.Ok(device);
    }

    private async static Task<IResult> CreateDevice(
        SaveDeviceCommand command,
        ISender sender)
    {
        var response = await sender.Send(command);
        return Results.Created($"/devices/{command.Id}", response);
    }
    
    private async static Task<IResult> UpdateDevice(
        SaveDeviceCommand command,
        ISender sender)
    {
        await sender.Send(command);
        return Results.NoContent();
    }
}