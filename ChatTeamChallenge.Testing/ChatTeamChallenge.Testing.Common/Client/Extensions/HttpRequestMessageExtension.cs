using System.Text;
using ChatTeamChallenge.Testing.Common.Client.Constants;
using Newtonsoft.Json;

namespace ChatTeamChallenge.Testing.Common.Client.Extensions;

public static class HttpRequestMessageExtension
{
    public static void SetJsonBody(this HttpRequestMessage request, object body)
    {
        var jsonBody = JsonConvert.SerializeObject(body);
        request.Content = new StringContent(jsonBody, Encoding.UTF8, EndpointApiConstants.MediaType);
    }
}