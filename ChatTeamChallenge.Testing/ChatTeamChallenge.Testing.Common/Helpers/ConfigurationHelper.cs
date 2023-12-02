using Microsoft.Extensions.Configuration;

namespace ChatTeamChallenge.Testing.Common.Helpers;

public static class ConfigurationHelper
{
    private static IConfiguration _configuration = null!;

    public static IConfiguration Configuration => _configuration ??= new ConfigurationBuilder()
        .AddJsonFile(GetConfigFileName())
        .Build();

    public static string AppUrl => GetAppSetting("BaseURL");
    
    public static string GetConfigFileName()
    {
#if LOCALDEBUG
        return "appsettings.local.json";
#else
        return "appsettings.json";
#endif
    }
    
    private static string GetAppSetting(string name)
    {
        return Configuration[name]!;
    }
}