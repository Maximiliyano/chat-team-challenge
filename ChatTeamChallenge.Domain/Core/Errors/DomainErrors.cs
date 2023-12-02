using System.Net;
using ChatTeamChallenge.Domain.Core.Primities;

namespace ChatTeamChallenge.Domain.Core.Errors;

public static class DomainErrors
{
    public static class General
    {
        public static Error ServerError => new(
            HttpStatusCode.InternalServerError,
            "The server encountered an unrecoverable error.");
    }
    
    public static class User
    {
        public static Error NotFound<T>(T entity) => new(
            HttpStatusCode.NotFound, 
            $"The user with ID: {entity} was not found.");

        public static Error RepeatError => new(
            HttpStatusCode.Conflict,
            "Entity with the same key is exist in system");
    }
    
    public static class Chat
    {
        public static Error NotFound => new(
            HttpStatusCode.NotFound, 
            "The chat with the specified identifier was not found.");
        
        public static Error TopicIsRequired => new(
            HttpStatusCode.BadRequest, 
            "The topic field is required.");
        
        public static Error TopicIsNotUnique(string topic) => new(
            HttpStatusCode.Conflict, 
            $"The chat with the specified topic {topic} was not unique.");
    }
    
    public static class ChatMember
    {
        public static Error NotFound => new(
            HttpStatusCode.NotFound, 
            "The chat member with the specified identifier was not found.");
        
        public static Error AlreadyAdded => new(
            HttpStatusCode.Conflict, 
            "The chat member with the specified identifier was already added.");
        
        public static Error IsNotAdded => new(
            HttpStatusCode.BadRequest,
            "The user is not added to this chat.");
    }
    
    public static class Message
    {
        public static Error NotFound(int id) => new(
            HttpStatusCode.NotFound, 
            $"The message with the specified identifier {id} was not found.");

        public static Error EmptyPassword => new(
            HttpStatusCode.BadRequest,
            "The password message is empty.");
        
        public static Error EmptyText => new(
            HttpStatusCode.BadRequest,
            "The text message is empty.");

        public static Error ZeroPageCount => new(
            HttpStatusCode.BadRequest,
            "The start from page doesn't below zero");
    }
    
    public static class Email
    {
        public static Error InvalidFormat => new(
            HttpStatusCode.BadRequest, 
            "The email format is invalid.");
    }
    
    public static class Authentication
    {
        public static Error InvalidEmailOrPassword => new(
            HttpStatusCode.BadRequest,
            "The specified email or password are incorrect.");

        public static Error IsEmailNotUnique => new(
            HttpStatusCode.BadRequest,
            "The specified email are exist in system.");
    }
    
    public static class RefreshToken
    {
        public static Error InvalidToken => new(
            HttpStatusCode.BadRequest,
            "The specified token is not valid.");
        
        public static Error Expired => new (
            HttpStatusCode.BadRequest,
            "The specified token is expired.");
    }
    
    public static class AccessToken
    {
        public static Error InvalidToken => new(
            HttpStatusCode.BadRequest,
            "The specified token is not valid.");
    }
}