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

        public static Error IsUsernameNotUnique => new(
            HttpStatusCode.Conflict,
            "The specified username are exist in system.");
        
        public static Error IsEmailNotUnique => new(
            HttpStatusCode.Conflict,
            "The specified email are exist in system.");
        
        public static Error RoleIsAlreadySet => new(
            HttpStatusCode.Conflict,
            "The role is already set in the system.");
    }
    
    public static class Chat
    {
        public static Error ImpossibleToDelete => new(
            HttpStatusCode.Conflict, 
            "The chat impossible to delete.");
        
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

        public static Error ChatIsNotFounded => new(
            HttpStatusCode.NotFound,
            "The chat is not founded in user");
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
    
    public static class File
    {
        public static Error WrongType => new(
            HttpStatusCode.BadRequest,
            "Incorrect file type. It is not support current extension.");
        
        public static Error Missing => new(
            HttpStatusCode.BadRequest,
            "The file is missing");

        public static Error MaximumSize(int maxSizeInMb) => new(
            HttpStatusCode.BadRequest,
            $"CommonFile is exceeded maximum size. Try to upload file less than {maxSizeInMb} MB");
    }
}