using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Message;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Domain.Interfaces;

public interface IMessageService
{
    Task<Result<int>> CreateAsync(int userId, CreationMessageRequest creationMessageRequest);
    Task<Result> EditAsync(int messageId, string body);
    Task<Result<PagedList<MessageModel>>> ReadAllAsync(int page, int pageSize, SearchMessageRequest? searchMessageDto);
    Task<Result<MessageModel>> ReadByIdAsync(int messageId);
    Task<Result> DeleteAsync(int messageId);
}