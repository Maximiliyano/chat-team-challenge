using System.ComponentModel.DataAnnotations;

namespace ChatTeamChallenge.Contracts.Message;

public sealed class CreationMessageFileRequest
{
    [Required]
    public required int MessageId { get; set; }

    [Url]
    public required string Url { get; set; }
}