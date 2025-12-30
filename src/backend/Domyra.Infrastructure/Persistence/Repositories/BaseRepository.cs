namespace Domyra.Infrastructure.Persistence.Repositories;

using Domyra.Application.Contracts.Repositories;
using Domain.Entities;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly DomyraContext Context;

    public BaseRepository(DomyraContext context)
    {
        Context = context;
    }
    
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await Context.Set<T>().AddAsync(entity, cancellationToken);   
    }

    public void MarkAsChanged(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
    }

    public async Task CommitChangesAsync(CancellationToken cancellationToken) =>
        await Context.SaveChangesAsync(cancellationToken);
}