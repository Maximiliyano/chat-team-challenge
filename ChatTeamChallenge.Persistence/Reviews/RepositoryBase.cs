using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Domain.Core.Primities;
using ChatTeamChallenge.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ChatTeamChallenge.Persistence.Reviews;

public abstract class RepositoryBase<TEntity>
    where TEntity : Entity
{
    protected RepositoryBase(IDbContext context) => DbContext = context;
    
    protected IDbContext DbContext { get; }

    public async Task<TEntity?> ReadByIdAsync(int id) =>
        await DbContext.ReadByIdAsync<TEntity>(id);
    
    public async Task InsertAsync(TEntity obj) =>
        await DbContext.InsertAsync(obj);
    
    public async Task RemoveAsync(TEntity obj) =>
        await DbContext.Set<TEntity>()
            .Where(e => e == obj)
            .ExecuteDeleteAsync();
    
    protected async Task<bool> AnyAsync(Specification<TEntity> specification) =>
        await DbContext.Set<TEntity>().AnyAsync(specification);
}