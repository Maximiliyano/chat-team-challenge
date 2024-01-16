using System.Reflection;
using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Domain.Core.Abstractions;
using ChatTeamChallenge.Domain.Core.Primities;
using ChatTeamChallenge.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace ChatTeamChallenge.Persistence;

public class ChatTeamChallengeDbContext : DbContext, IDbContext, IUnitOfWork
{
    private readonly IMediator _mediator;

    public ChatTeamChallengeDbContext(
        IMediator mediator,
        DbContextOptions options) : base(options)
    {
        _mediator = mediator;
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) =>
        Database.BeginTransactionAsync(cancellationToken);

    public new DbSet<TEntity> Set<TEntity>()
        where TEntity : Entity
        => base.Set<TEntity>();

    public async Task<TEntity?> ReadByIdAsync<TEntity>(int id)
        where TEntity : Entity
        => id == 0
            ? null
            : await Set<TEntity>().FindAsync(id);

    public async Task InsertAsync<TEntity>(TEntity entity) 
        where TEntity : Entity 
        => await Set<TEntity>().AddAsync(entity);

    public async Task InsertRangeAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : Entity
        => await Set<TEntity>().AddRangeAsync(entities);

    public async Task RemoveAsync<TEntity>(TEntity entity)
        where TEntity : Entity
        => await Set<TEntity>()
            .Where(e => e == entity)
            .ExecuteDeleteAsync();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.Seed();
        
        base.OnModelCreating(modelBuilder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var utcNow = DateTime.UtcNow;
        
        UpdateAuditableEntities(utcNow);
        
        UpdateSoftDeletableEntities(utcNow);

        await PublishDomainEvents(cancellationToken);
        
        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    {
        var aggregateRoots = ChangeTracker
            .Entries<AggregateRoot>()
            .Where(entityEntry => entityEntry.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = aggregateRoots.SelectMany(entityEntry => entityEntry.Entity.DomainEvents).ToList();
        
        aggregateRoots.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());

        var tasks = domainEvents.Select(domainEvent => _mediator.Publish(domainEvent, cancellationToken));

        await Task.WhenAll(tasks);
    }

    private void UpdateAuditableEntities(DateTime utcNow)
    {
        foreach (var entityEntry in ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(IAuditableEntity.CreatedAt)).CurrentValue = utcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IAuditableEntity.UpdatedAt)).CurrentValue = utcNow;
            }
        }
    }

    private void UpdateSoftDeletableEntities(DateTime utcNow)
    {
        foreach (var entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>())
        {
            if (entityEntry.State != EntityState.Deleted)
            {
                continue;
            }

            entityEntry.Property(nameof(ISoftDeletableEntity.DeletedAt)).CurrentValue = utcNow;

            entityEntry.Property(nameof(ISoftDeletableEntity.Deleted)).CurrentValue = true;

            entityEntry.State = EntityState.Modified;

            UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
        }
    }

    private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
    {
        if (!entityEntry.References.Any())
        {
            return;
        }

        foreach (var referenceEntry in entityEntry.References
                     .Where(r => r.TargetEntry!.State == EntityState.Deleted))
        {
            referenceEntry.TargetEntry!.State = EntityState.Unchanged;
            
            UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
        }
    }
}