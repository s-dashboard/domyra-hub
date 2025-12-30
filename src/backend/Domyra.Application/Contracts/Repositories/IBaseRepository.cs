using Domyra.Domain.Entities;

namespace Domyra.Application.Contracts.Repositories;

public interface IBaseRepository<in T> where T : BaseEntity
{
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task CommitChangesAsync(CancellationToken cancellationToken);
    void MarkAsChanged(T entity);
}