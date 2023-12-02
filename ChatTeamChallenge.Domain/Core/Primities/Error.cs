using System.Net;

namespace ChatTeamChallenge.Domain.Core.Primities;

public sealed class Error : ValueObject
{
    public Error(HttpStatusCode code, string message)
    {
        Code = (int)code;
        Message = message;
    }
    
    public int Code { get; }

    public string Message { get; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return Message;
    }

    internal static Error None => null!;
}