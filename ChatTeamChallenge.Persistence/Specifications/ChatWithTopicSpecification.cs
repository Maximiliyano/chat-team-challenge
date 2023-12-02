using System.Linq.Expressions;
using ChatTeamChallenge.Domain.Apartments;

namespace ChatTeamChallenge.Persistence.Specifications;

public sealed class ChatWithTopicSpecification : Specification<Chat>
{
    private readonly string _topic;

    internal ChatWithTopicSpecification(string topic) => _topic = topic;

    protected override Expression<Func<Chat, bool>> ToExpression() => chat => chat.Topic == _topic;
}