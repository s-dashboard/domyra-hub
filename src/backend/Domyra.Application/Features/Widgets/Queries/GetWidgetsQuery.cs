using Domyra.Application.Features.Widgets.Models;

namespace Domyra.Application.Features.Widgets.Queries;

using MediatR;

public sealed record GetWidgetsQuery(GetWidgetsQueryParams Parameters) : IRequest<IEnumerable<WidgetModel>>;