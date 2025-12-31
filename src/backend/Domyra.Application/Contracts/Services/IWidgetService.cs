namespace Domyra.Application.Contracts.Services;

public interface IWidgetService
{
    Task ActivateAsync(string widgetAlias, CancellationToken cancellationToken);
}