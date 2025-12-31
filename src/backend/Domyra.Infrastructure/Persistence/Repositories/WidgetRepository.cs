using Domyra.Application.Contracts.Repositories;
using Domyra.Application.Features.Widgets.Models;
using Domyra.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domyra.Infrastructure.Persistence.Repositories;

public class WidgetRepository : BaseRepository<Widget>, IWidgetRepository
{
    public WidgetRepository(DomyraContext context) : base(context)
    {
    }

    public async Task<Widget?> FetchSingleAsync(string alias, CancellationToken cancellationToken) =>
        await Context.Widgets.SingleOrDefaultAsync(p => p.Alias == alias, cancellationToken);

    public async Task<IEnumerable<Widget>> FetchAllAsync(GetWidgetsQueryParams parameters, CancellationToken cancellationToken)
    {
        var query = Context.Widgets.AsQueryable();
        var term = parameters.Filter?.ToLower();
        
        if (!string.IsNullOrWhiteSpace(term))
        {
            query = query.Where(p =>
                p.Alias.ToLower(
                ).Contains(term) || (p.Description != null && p.Description.ToLower().Contains(term))
            );    
        }

        if (parameters.State.HasValue)
        {
            query = query.Where(p =>
                p.State == parameters.State.Value
            );
        }
        
        return await query.OrderBy(d => d.Id)
            .ToListAsync(cancellationToken);
    }
}