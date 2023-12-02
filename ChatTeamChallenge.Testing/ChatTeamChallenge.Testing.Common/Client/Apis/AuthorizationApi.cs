using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Responses;

namespace ChatTeamChallenge.Testing.Common.Client.Apis;

public sealed class AuthorizationApi : ClientApi
{
    public AuthorizationApi() : base(ApiRoutes.Auth.Base)
    {
    }

    public async Task<Response<AuthUserResponse>> RegisterAsync(RegisterRequest registerRequest)
    {
        var request = CreatePostRequest(ApiRoutes.Auth.Register, registerRequest);
        return await ExecuteRequest<AuthUserResponse>(request);
    }
}