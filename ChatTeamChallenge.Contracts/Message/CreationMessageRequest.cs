using System.ComponentModel.DataAnnotations;

namespace ChatTeamChallenge.Contracts.Message;

public sealed class CreationMessageRequest
{
    [Required]
    [MinLength(1)]
    [MaxLength(2000)]
    public required string Body { get; init; }
    
    [Required]
    public required int ChatId { get; init; }
    
    public int? ReceiverId { get; init; } 
    
    public CreationMessageFileRequest? File { get; init; }
}