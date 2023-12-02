using Newtonsoft.Json;

namespace ChatTeamChallenge.Testing.Common.Client.Extensions;

public static class HttpResponseMessageExtension
{
    public static async Task<T> ExecuteJsonBody<T>(this HttpResponseMessage response) 
        where T : class?
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content)!;
    }
}