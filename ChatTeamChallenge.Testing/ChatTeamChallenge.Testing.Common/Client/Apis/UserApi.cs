using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Testing.Common.Client.Apis;

public sealed class UserApi : ClientApi
{
    public UserApi() : base(ApiRoutes.User.Base)
    {
    }

    public async Task<Response<UserModel?>> GetByIdAsync(int userId)
    {
        var request = CreateGetRequest($"u/{userId}"); // TODO write constant
        return await ExecuteRequest<UserModel?>(request);
    }
    
    public async Task<HttpResponseMessage> DeleteAsync(int userId)
    {
        var request = CreateDeleteRequest(string.Empty);
        
        // TODO add query $"?{nameof(userId)}={userId}"
        
        return await ExecuteRequest(request);
    }
}