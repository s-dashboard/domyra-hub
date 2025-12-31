using Domyra.Application.Contracts.Repositories;
using Domyra.Application.Contracts.Services;
using Domyra.Domain.Enums;

namespace Domyra.Application.Features.Widgets.Services;

public class WidgetService : IWidgetService
{
    private readonly IWidgetRepository _widgetRepository;

    public WidgetService(IWidgetRepository widgetRepository)
    {
        _widgetRepository = widgetRepository;
    }
    
    public async Task ActivateAsync(string widgetAlias, CancellationToken cancellationToken)
    {
        var widget = await _widgetRepository.FetchSingleAsync(widgetAlias, cancellationToken);
        if (widget == null)
        {
            throw new InvalidOperationException($"Widget with alias '{widgetAlias}' not found");
        }

        widget.State = widget.State == WidgetState.Inactive ? WidgetState.Active : WidgetState.Inactive;
        _widgetRepository.MarkAsChanged(widget);
        await _widgetRepository.CommitChangesAsync(cancellationToken);
    }
}