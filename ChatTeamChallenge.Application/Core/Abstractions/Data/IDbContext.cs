using ChatTeamChallenge.Domain.Core.Primities;
using Microsoft.EntityFrameworkCore;

namespace ChatTeamChallenge.Application.Core.Abstractions.Data;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : Entity;
    
    Task<TEntity?> ReadByIdAsync<TEntity>(int id)
        where TEntity : Entity;
    
    Task InsertAsync<TEntity>(TEntity entity) 
        where TEntity : Entity;
    
    Task InsertRangeAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : Entity;
    
    Task RemoveAsync<TEntity>(TEntity entity)
        where TEntity : Entity;
}