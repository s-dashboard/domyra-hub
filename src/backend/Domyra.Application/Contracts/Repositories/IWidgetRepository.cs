using Domyra.Application.Features.Widgets.Models;
using Domyra.Domain.Entities;

namespace Domyra.Application.Contracts.Repositories;

public interface IWidgetRepository
    : IBaseRepository<Widget>
{
    Task<Widget?> FetchSingleAsync(string alias, CancellationToken cancellationToken);
    Task<IEnumerable<Widget>> FetchAllAsync(GetWidgetsQueryParams parameters, CancellationToken cancellationToken);
}