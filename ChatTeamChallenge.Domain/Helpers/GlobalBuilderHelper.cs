using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.ChatMember;
using ChatTeamChallenge.Contracts.Message;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Models;
using ChatTeamChallenge.Domain.Responses;

namespace ChatTeamChallenge.Domain.Helpers;

public static class GlobalBuilderHelper
{
    public static AuthUserResponse BuildAuthUserDto(UserModel userModel, AccessTokenResponse accessTokenResponse) =>
        new()
        {
            User = userModel,
            Token = accessTokenResponse
        };

    public static RefreshToken BuildRefreshToken(string token, int userId) =>
        new()
        {
            Token = token,
            UserId = userId
        };

    public static AccessTokenResponse BuildAccessTokenDto(AccessToken accessToken, string token) =>
        new(accessToken, token);

    public static SearchMessageRequest BuildSearchMessageDto(int? senderId = null, int? receiverId = null, int? chatId = null, bool? isRead = null, DateTime? date = null) =>
        new()
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            ChatId = chatId,
            IsRead = isRead,
            Date = date
        };

    public static ChatMemberRequest BuildChatMemberRequest(int userId, int chatId) =>
        new()
        {
            UserId = userId,
            ChatId = chatId
        };
}