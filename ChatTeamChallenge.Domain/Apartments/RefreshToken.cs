using ChatTeamChallenge.Domain.Core.Primities;

namespace ChatTeamChallenge.Domain.Apartments;

public sealed class RefreshToken : Entity
{
    private readonly DateTime _createdAt;

    private const int DaysToExpire = 5;

    public required string Token { get; set; }

    public RefreshToken() => 
        CreatedAt = DateTime.UtcNow;
    
    public DateTime CreatedAt
    {
        get => _createdAt;
        init => _createdAt = value == DateTime.MinValue ? DateTime.UtcNow : value;
    }
    
    public DateTime? UpdatedAt { get; set; }

    public DateTime Expires { get; private set; } = DateTime.UtcNow.AddDays(DaysToExpire);

    public int UserId { get; set; }
  
    public User User { get; set; } = null!;

    public bool IsActive => DateTime.UtcNow <= Expires;
}