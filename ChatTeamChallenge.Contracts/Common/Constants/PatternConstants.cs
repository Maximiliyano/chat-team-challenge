namespace ChatTeamChallenge.Contracts.Common.Constants;

public static class PatternConstants
{
    public const string EmailPattern = "^[A-Za-z0-9\\._\\-]+@([a-zA-Z0-9\\-]+\\.{1})+[a-zA-Z0-9\\-]{2,}$";

    public const string UsernamePattern = "^[A-Za-z0-9._]{3,24}$";
    
    public const string PasswordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,16}$";
}