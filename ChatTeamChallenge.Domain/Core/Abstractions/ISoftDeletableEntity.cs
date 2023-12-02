namespace ChatTeamChallenge.Domain.Core.Abstractions;

public interface ISoftDeletableEntity
{
    DateTime DeletedAt { get; }
    
    bool Deleted { get; }
}