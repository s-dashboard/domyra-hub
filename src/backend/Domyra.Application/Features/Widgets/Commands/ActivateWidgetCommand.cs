namespace Domyra.Application.Features.Widgets.Commands;

using MediatR;

public sealed record ActivateWidgetCommand(string WidgetAlias) :
    IRequest; 