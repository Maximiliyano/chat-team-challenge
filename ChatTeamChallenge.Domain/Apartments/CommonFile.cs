using ChatTeamChallenge.Domain.Core.Primities;

namespace ChatTeamChallenge.Domain.Apartments;

public class CommonFile : Entity
{
    public required string Name { get; set; }
    
    public required string ContentType { get; set; }
    
    public required byte[] Data { get; set; }
}