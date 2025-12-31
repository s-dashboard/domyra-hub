using Domyra.Application.Features.Widgets.Commands;
using Domyra.Application.Features.Widgets.Models;
using Domyra.Application.Features.Widgets.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Domyra.Api;

public static class WidgetEndpoints
{
    public static IEndpointRouteBuilder MapWidgetEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("api/widgets")
            .WithTags("Widgets");
        
        group.MapGet("/", GetWidgets);
        group.MapPut("/{id}/activate", ActivateWidget);

        return endpoints;
    }

    private async static Task<IResult> GetWidgets([AsParameters]GetWidgetsQueryParams parameters, ISender sender)
    {
        var widgets = await sender.Send(new GetWidgetsQuery(parameters));

        return Results.Ok(widgets);
    }

    private async static Task<IResult> ActivateWidget(
        string id,
        ISender sender)
    {
        await sender.Send(new ActivateWidgetCommand(id));
        return Results.NoContent();
    }
}