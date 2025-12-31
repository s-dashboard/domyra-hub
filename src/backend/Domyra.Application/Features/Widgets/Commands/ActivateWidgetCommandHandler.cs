namespace Domyra.Application.Features.Widgets.Commands;

using Contracts.Services;
using MediatR;

public class ActivateWidgetCommandHandler : IRequestHandler<ActivateWidgetCommand>
{
    private readonly IWidgetService _widgetService;
    
    public ActivateWidgetCommandHandler(IWidgetService widgetService)
    {
        _widgetService = widgetService;
    }
    
    public async Task Handle(ActivateWidgetCommand request, CancellationToken cancellationToken)
    {
        await _widgetService.ActivateAsync(request.WidgetAlias, cancellationToken);
    }
}