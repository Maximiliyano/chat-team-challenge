using ChatTeamChallenge.Contracts.Chat;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Domain.Reviews;

public interface IChatRepository
{
    Task InsertAsync(Chat chat);

    Task UpdateAsync(UpdateChatRequest updateChatRequest);
    
    Task<Chat?> ReadByIdAsync(int id);
    
    Task<Chat?> ReadByIdWithMessagesAsync(int id);
    
    /// <summary>
    /// Checks if the specified topic is unique.
    /// </summary>
    /// <param name="topic">The topic.</param>
    /// <returns>True if the specified topic is unique, otherwise false.</returns>
    Task<Chat?> ReadByTopicAsync(string topic);
    
    /// <summary>
    /// Checks if the specified topic is unique.
    /// </summary>
    /// <param name="topic">The topic.</param>
    /// <returns>True if the specified topic is unique, otherwise false.</returns>
    Task<bool> IsTopicUniqueAsync(string topic);
    
    Task<PagedList<ChatModel>> ReadByAllAsync(int page, int pageSize, DateTime? date, bool? isPublic, int? userId);
    
    Task RemoveAsync(Chat chat);
}