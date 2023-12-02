namespace ChatTeamChallenge.Domain.Core.Primities;

public abstract class BaseModel
{
    private readonly DateTime _createdAt;
    
    protected BaseModel()
    {
        CreatedAt = DateTime.UtcNow;
    }
    
    public int Id { get; set; }

    public DateTime CreatedAt
    {
        get => _createdAt;
        init => _createdAt = value == DateTime.MinValue ? DateTime.UtcNow : value;
    }
    
    public DateTime? UpdatedAt { get; set; }
}