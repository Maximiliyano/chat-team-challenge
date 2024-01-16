using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using Microsoft.AspNetCore.Http;

namespace ChatTeamChallenge.Domain.Interfaces;

public interface IMessageFileService
{
    Task<Result<MessageFile>> GetById(int fileId);
    
    Task<Result<PagedList<MessageFile>>> GetAll(int page, int pageSize);

    Task<Result<int>> Upload(IFormFile file, FileType fileType, int identifier);

    Task<Result> Remove(int fileId);
}