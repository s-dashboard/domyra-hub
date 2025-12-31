using Domyra.Domain.Enums;

namespace Domyra.Application.Features.Widgets.Models;

public sealed record GetWidgetsQueryParams(
    string? Filter,
    WidgetState? State 
);