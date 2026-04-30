using System.Collections.Concurrent;
using core.Entities;
using core.Interfaces;
using infrastructure.Repositories;

namespace infrastructure.Data;

public class UnitOfWork(MDBContext context) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repos = [];
    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;

        return (IGenericRepository<TEntity>)_repos.GetOrAdd(type, t =>
        {
            var repoType = typeof(GenericRepository<>).MakeGenericType(typeof(TEntity));

            return Activator.CreateInstance(repoType, context)
                ?? throw new InvalidOperationException($"Kunde inte skapa en instans för repository {t}");
        });
    }
}
