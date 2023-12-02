using ChatTeamChallenge.Testing.Common.Client.Extensions;
using ChatTeamChallenge.Testing.Common.Helpers;

namespace ChatTeamChallenge.Testing.Common.Client;

public abstract class ClientApi
{
    private readonly HttpClient _client;
    
    private readonly string _serviceUrl;

    protected ClientApi(string serviceUrl)
    {
        _serviceUrl = serviceUrl;
        _client = new HttpClient
        {
            BaseAddress = new Uri(ConfigurationHelper.AppUrl)
        };
    }

    #region Requests
    
    protected HttpRequestMessage CreatePostRequest<TPayload>(string url, TPayload payload)
        where TPayload : class
    {
        return CreateRequestWithServiceUrl(HttpMethod.Post, url, payload);
    }

    protected HttpRequestMessage CreateDeleteRequest(string url)
    {
        return CreateRequestWithServiceUrl(HttpMethod.Delete, url);
    }

    protected HttpRequestMessage CreateGetRequest(string url)
    {
        return CreateRequestWithServiceUrl(HttpMethod.Get, url);
    }
    
    #endregion
    
    #region Core

    private HttpRequestMessage CreateRequestWithServiceUrl<TPayload>(HttpMethod method, string url, TPayload payload)
        where TPayload : class
    {
        return CreateRequest(method, CreateServiceUrl(url), payload);
    }
    
    private HttpRequestMessage CreateRequestWithServiceUrl(HttpMethod method, string url)
    {
        return CreateRequest(method, CreateServiceUrl(url));
    }
    
    private static HttpRequestMessage CreateRequest<TPayload>(HttpMethod method, string requestUrl, TPayload payload)
        where TPayload : class
    {
        var request = new HttpRequestMessage(method, requestUrl);
        
        request.SetJsonBody(payload);
        
        return request;
    }
    
    private static HttpRequestMessage CreateRequest(HttpMethod method, string requestUrl)
    {
        return new HttpRequestMessage(method, requestUrl);
    }

    private string CreateServiceUrl(string url)
    {
        return Path.Combine(_serviceUrl, url);
    }
    
    #endregion

    #region ExecuteRequests

    protected async Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage request)
    {
        var response = await _client.SendAsync(request);
        return response;
    }
    
    protected async Task<Response<TResponse>> ExecuteRequest<TResponse>(HttpRequestMessage request)
        where TResponse : class?
    {
        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode) 
            return new Response<TResponse>(response.StatusCode);
        
        var data = await response.ExecuteJsonBody<TResponse>();
        return new Response<TResponse>(data, response.StatusCode);
    }

    #endregion
}