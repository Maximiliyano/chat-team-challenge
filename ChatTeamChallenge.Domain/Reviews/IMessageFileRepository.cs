using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;

namespace ChatTeamChallenge.Domain.Reviews;

public interface IMessageFileRepository
{
    Task InsertAsync(MessageFile file);
    Task<MessageFile?> ReadByIdAsync(int fileId);
    Task<PagedList<MessageFile>> ReadAllAsync(int page, int pageSize);
    Task RemoveAsync(MessageFile file);
}