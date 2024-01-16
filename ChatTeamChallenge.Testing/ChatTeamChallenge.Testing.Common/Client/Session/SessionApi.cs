using ChatTeamChallenge.Testing.Common.Client.Constants;
using Newtonsoft.Json.Linq;

namespace ChatTeamChallenge.Testing.Common.Client.Session;

public sealed class SessionApi : ClientApi
{
    public SessionStorage SessionStorage { get; } = new ();
    
    public SessionApi() 
        : base(EndpointApiConstants.SessionUrl)
    {
    }

    public async Task<Response<JObject>> SignIn(string userName, string password)
    {
        var signInModel = new SignInModel
        {
            UserName = userName,
            Password = password
            // TODO captcha
        };

        var signInRequest = CreatePostRequest(signInModel);
        var signInResponse = await ExecuteRequest<JObject>(signInRequest);

        SessionStorage.Token = JObject.Parse(signInResponse.Content.ReadAsStringAsync().ToString()!).GetValue("access_token")!.Value<string>();

        return signInResponse;
    }
}