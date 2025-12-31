using Domyra.Domain.Entities;
using Domyra.Domain.Enums;

namespace Domyra.Application.Features.Widgets.Models;

public sealed record WidgetModel(
    int Id,
    string Alias,
    WidgetState State,
    string? Kind,
    string? ElementName,
    string? ScriptUrl,
    string? Html,
    int Cols,
    int Rows
    )
{
    public static WidgetModel From(Widget widget) =>
        new(
            widget.Id,
            widget.Alias,
            widget.State,
            widget.Kind,
            widget.ElementName,
            widget.ScriptUrl,
            widget.Html,
            widget.Cols,
            widget.Rows
        );
}
