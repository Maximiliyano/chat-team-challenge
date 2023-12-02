using ChatTeamChallenge.Contracts.Chat;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Domain.Interfaces;

public interface IChatService
{
    Task<Result<int>> Create(CreationChatRequest creationChatRequest);
    Task<Result> Update(UpdateChatRequest updateChatRequest); 
    Task<Result<ChatModel>> ReadByIdAsync(int id);
    Task<Result<ChatModel>> ReadByTopicAsync(string topic);
    Task<Result<PagedList<ChatModel>>> ReadAllAsync(int page, int pageSize, DateTime? dateTime, bool? isPublic, int? userId);
    Task<Result> RemoveAsync(int id);
}