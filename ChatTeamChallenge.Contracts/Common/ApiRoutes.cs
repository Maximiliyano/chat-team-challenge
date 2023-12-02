namespace ChatTeamChallenge.Contracts.Common;

public static class ApiRoutes
{
    public static class Auth
    {
        public const string Base = "api/auth";
        
        public const string Login = "login";
        
        public const string Register = "register";
    }

    public static class Chat
    {
        public const string Create = "new";

        public const string Update = "upd";
        
        public const string Get = "c/{id:int}";
        
        public const string GetAll = "inbox";
        
        public const string Remove = "r/{id:int}";
    }
    
    public static class ChatMember
    {
        public const string Add = "add";
        
        public const string Get = "info/{userId:int}/{chatId:int}";
        
        public const string GetAll = "all";
        
        public const string Remove = "remove";
    }
    
    public static class Message
    {
        public const string Create = "add/{senderId:int}";
        
        public const string SearchBy = "s/{messageId:int}";
        
        public const string GetAll = "all";
        
        public const string Update = "u/{messageId:int}";
        
        public const string Remove = "r/{messageId:int}";
    }
    
    public static class Token
    {
        public const string Refresh = "refresh";
        
        public const string Revoke = "revoke";
    }
    
    public static class User
    {
        public const string Base = "api/user";
        
        public const string GetById = "u/{id:int}";
        
        public const string GetByEmail = "ue/{email}";
        
        public const string GetAll = "list";
        
        public const string GetFromToken = "fromToken";
        
        public const string ChangePassword = "chpass/{userId:int}";
    }
}