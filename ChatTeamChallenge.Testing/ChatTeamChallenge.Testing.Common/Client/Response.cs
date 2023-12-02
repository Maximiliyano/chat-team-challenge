using System.Net;

namespace ChatTeamChallenge.Testing.Common.Client;

public sealed class Response<T> : HttpResponseMessage
    where T : class?
{
    public Response(T data, HttpStatusCode statusCode)
    {
        Data = data;
        StatusCode = statusCode;
    }

    public Response(HttpStatusCode statusCode)
    {
        Data = default!;
        StatusCode = statusCode;
    }
    
    public T Data { get; }
}