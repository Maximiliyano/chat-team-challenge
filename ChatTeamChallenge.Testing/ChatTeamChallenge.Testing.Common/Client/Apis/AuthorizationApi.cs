using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Responses;
using ChatTeamChallenge.Testing.Common.Client.Session;

namespace ChatTeamChallenge.Testing.Common.Client.Apis;

public sealed class AuthorizationApi : ClientApi
{
    public AuthorizationApi(SessionStorage sessionStorage) 
        : base(ApiRoutes.Auth.Base, sessionStorage)
    {
    }

    public async Task<Response<AuthUserResponse>> RegisterAsync(RegisterRequest registerRequest)
    {
        var request = CreatePostRequest(ApiRoutes.Auth.Register, registerRequest);
        return await ExecuteRequest<AuthUserResponse>(request);
    }
}