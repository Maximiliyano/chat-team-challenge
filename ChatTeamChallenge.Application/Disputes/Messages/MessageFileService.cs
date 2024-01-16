using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Domain.Reviews;
using Microsoft.AspNetCore.Http;

namespace ChatTeamChallenge.Application.Disputes.Messages;

public sealed class MessageFileService : IMessageFileService
{
    private readonly IMessageFileRepository _messageFileRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    private string[] ImageTypeAllowedExtensions { get; } =
    {
        "jpg",
        "png",
        "jpeg"
    };
    
    public MessageFileService(
        IMessageFileRepository messageFileRepository, 
        IUnitOfWork unitOfWork)
    {
        _messageFileRepository = messageFileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MessageFile>> GetById(int fileId)
    { 
        var file = await _messageFileRepository.ReadByIdAsync(fileId);
        return file is null ? 
            Result.Failure<MessageFile>(DomainErrors.File.Missing) : 
            Result.Success(file);
    }

    public async Task<Result<PagedList<MessageFile>>> GetAll(int page, int pageSize)
    {
        return await _messageFileRepository.ReadAllAsync(page, pageSize);
    }

    public async Task<Result<int>> Upload(IFormFile file, FileType fileType, int identifier)
    {
        // Check maximum size
        const int maximumSize = 5 * 1024 * 1024;
        if (file.Length > maximumSize)
        {
            return Result.Failure<int>(DomainErrors.File.MaximumSize(5));
        }
        
        // Check extensions
        if (fileType == FileType.UserProfile)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            if (!ImageTypeAllowedExtensions.Contains(fileExtension))
            {
                return Result.Failure<int>(DomainErrors.File.WrongType);
            }
        }

        using var memoryStream = new MemoryStream();
            
        await file.CopyToAsync(memoryStream);

        var creationFileModel = MessageFile.Create(
            identifier,
            file.FileName,
            file.ContentType,
            memoryStream.ToArray());
        
        await _messageFileRepository.InsertAsync(creationFileModel);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(creationFileModel.Id);
    }

    public async Task<Result> Remove(int fileId)
    {
        var file = await GetById(fileId);

        if (file.IsFailure)
        {
            return Result.Failure(DomainErrors.File.Missing);
        }
        
        await _messageFileRepository.RemoveAsync(file.Value);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}