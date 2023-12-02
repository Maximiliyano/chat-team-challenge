namespace ChatTeamChallenge.Domain.Core.Abstractions;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
}