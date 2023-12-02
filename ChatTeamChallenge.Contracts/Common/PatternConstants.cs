namespace ChatTeamChallenge.Contracts.Common;

public static class PatternConstants
{
    public const string EmailPattern = "^[A-Za-z0-9\\._\\-]+@([a-zA-Z0-9\\-]+\\.{1})+[a-zA-Z0-9\\-]{2,}$";
    
    public const string PasswordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,16}$";
}